using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using a3innuva.TAA.Migration.SDK.Implementations;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Serialization.Tests
{
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "a3innuvaSerializationBinder")]

    public class a3innuvaSerializationBinderTests
    {
        private a3innuvaSerializationBinder binder;

        public a3innuvaSerializationBinderTests()
        {
            this.binder = new a3innuvaSerializationBinder();
        }
        
        ~ a3innuvaSerializationBinderTests()
        {
            this.binder = null;
        }

        [Fact(DisplayName = "Check number bindings")]
        public void Check_number_bindings()
        {
            this.binder.KnownTypes.Count.Should().Be(25);
        }

        [Theory(DisplayName = "Check IMigrationEntity implementations are bindex")]
        [InlineData(typeof(IMigrationInfo))]
        [InlineData(typeof(IMigrationSet))]
        [InlineData(typeof(IMigrationEntity))]
        public void ChekMigrationEntityBinded(Type interfaceType)
        {
            Assembly interfacesAssembly = interfaceType.Assembly;

            Assembly implementationAssembly = Assembly.GetAssembly(typeof(MigrationInfo));
            var migrationEntityImplementations = this.GetImplementationsOrSubclasses(implementationAssembly, interfaceType);
            
            foreach (var type in migrationEntityImplementations)
            {
                this.binder.KnownTypes.Should().ContainSingle(x => x.FullName == type.FullName);
                this.CheckSDKType(interfacesAssembly, implementationAssembly, type);
            }
        }

        /// <summary>
        /// Check type properties are binded
        /// </summary>
        /// <param name="interfacesAssembly"></param>
        /// <param name="implementationAssembly"></param>
        /// <param name="type"></param>
        private void CheckSDKType(Assembly interfacesAssembly, Assembly implementationAssembly, Type type)
        {
            var properties = type.Properties();
            foreach (var property in properties)
            {
                var checkTypes = this.GetCheckType(property);
                foreach (var checkType in checkTypes)
                {
                    if (checkType.IsArray)
                    {
                        this.binder.KnownTypes.Should()
                            .ContainSingle(x => x.FullName == checkType.FullName);
                    }
                    else if (checkType.Assembly == interfacesAssembly && checkType.IsInterface)
                    {
                        this.CheckSDKTypeImplementations(implementationAssembly, checkType);

                        this.CheckSDKType(interfacesAssembly, implementationAssembly, checkType);
                    }
                }
            }
        }

        /// <summary>
        /// Check type implementation is binded
        /// </summary>
        /// <param name="implementationAssembly"></param>
        /// <param name="checkType"></param>
        private void CheckSDKTypeImplementations(Assembly implementationAssembly, Type checkType)
        {
            var implementations = this.GetImplementationsOrSubclasses(implementationAssembly, checkType);
            implementations.Count.Should().BeGreaterThan(0);
            foreach (var propertyTypeImplementation in implementations)
            {
                this.binder.KnownTypes.Should()
                    .ContainSingle(x => x.FullName == propertyTypeImplementation.FullName);
            }
        }

        /// <summary>
        /// Return types must be binded.
        /// If the property type is array then this method will return the array type and the element type.
        ///
        /// If the property type is not an array, then this method will return the property type.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private Type[] GetCheckType(PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            if (propertyType.IsArray)
            {
                return new[]
                {
                    propertyType,
                    propertyType.GetElementType()
                };
            }
            else
            {
                return new[] { propertyType };
            }
        }

        /// <summary>
        /// Get the non abstract classes from a base class or interface impplementations
        /// </summary>
        /// <param name="implementationAssembly"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private List<Type> GetImplementationsOrSubclasses(Assembly implementationAssembly, Type baseType)
        {
            List<Type> types = new List<Type>();
            
            foreach (var type in implementationAssembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                {
                    continue;
                }

                if (type.IsAssignableTo(baseType))
                {
                    types.Add(type);
                }
            }

            return types;
        }
    }
}
