namespace a3innuva.TAA.Migration.SDK.Extensions.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using FluentAssertions;
    using Xunit;

    public class MigrationSetExtensionsTests
    {

        [Theory(DisplayName = "Validate info")]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.ChartOfAccount, "vatNumber", 0, "2.0", true)]
        [InlineData((MigrationOrigin)1, MigrationType.ChartOfAccount, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.None, MigrationType.ChartOfAccount, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.None, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.ChartOfAccount, "", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.ChartOfAccount, "vatNumber", 10, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.ChartOfAccount, "vatNumber", 0, "1.0", false)]

        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.Journal, "vatNumber", 2010, "2.0", true)]
        [InlineData((MigrationOrigin)1, MigrationType.Journal, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.None, MigrationType.Journal, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3InnuvaFactura, MigrationType.None, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.Journal, "", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.Journal, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.Journal, "vatNumber", 2010, "1.0", false)]

        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.InputInvoice, "vatNumber", 2010, "2.0", true)]
        [InlineData((MigrationOrigin)1, MigrationType.InputInvoice, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.None, MigrationType.InputInvoice, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.Extern, MigrationType.None, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.InputInvoice, "", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.InputInvoice, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.InputInvoice, "vatNumber", 2010, "1.0", false)]

        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.OutputInvoice, "vatNumber", 2010, "2.0", true)]
        [InlineData((MigrationOrigin)1, MigrationType.OutputInvoice, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.None, MigrationType.OutputInvoice, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.None, "vatNumber", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.OutputInvoice, "", 2010, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.OutputInvoice, "vatNumber", 0, "2.0", false)]
        [InlineData(MigrationOrigin.A3ASESORnom, MigrationType.OutputInvoice, "vatNumber", 2010, "1.0", false)]
        public void ValidateInfo(MigrationOrigin origin, MigrationType type, string vatNumber, int year, string version, bool isValid)
        {
            IMigrationInfo info  = new MigrationInfo()
            {
                Origin = origin,
                Type = type, 
                Year = year, 
                VatNumber = vatNumber,
                Version = version
            };

            info.IsValid().Should().Be(isValid);
        }

        [Fact(DisplayName = "Validate entities account succeed")]
        public void Validate_entities_account_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.ChartOfAccount,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            Account account1 = new Account()
            {
                Code = "4301",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr"
            };

            Account account2 = new Account()
            {
                Code = "4300",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 2,
                Source = "scr"
            };

            set.Entities = new IMigrationEntity[2]{account1,account2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate entities account bad info failed")]
        public void Validate_entities_account_bad_info_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.ChartOfAccount,
                    VatNumber = "vatNumber",
                    Version = "1.0"
                },
            };

            Account account1 = new Account()
            {
                Code = "4301",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr"
            };

            Account account2 = new Account()
            {
                Code = "4300",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 2,
                Source = "scr"
            };

            set.Entities = new IMigrationEntity[2]{account1,account2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate entities account failed")]
        public void Validate_entities_account_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.ChartOfAccount,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            Account account1 = new Account()
            {
                Code = "4301",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr"
            };

            Account account2 = new Account()
            {
                Code = "asd",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 2,
                Source = "scr"
            };

            set.Entities = new IMigrationEntity[2]{account1,account2};

            var result = set.IsValid();

            result.errors.Any().Should().BeTrue();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate content and type account succeed")]
        public void Validate_content_and_type_account_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.ChartOfAccount,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            Account account1 = new Account()
            {
                Code = "4301",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr"
            };

            Account account2 = new Account()
            {
                Code = "4300",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 2,
                Source = "scr"
            };

            set.Entities = new IMigrationEntity[2]{account1,account2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate content and type account failed")]
        public void Validate_content_and_type_account_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.ChartOfAccount,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            Account account1 = new Account()
            {
                Code = "4301",
                Description = "desc",
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr"
            };

            Journal account2 = new Journal()
            {
            };

            set.Entities = new IMigrationEntity[2]{account1,account2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeFalse();
        }

        
        [Fact(DisplayName = "Validate entities journal succeed")]
        public void Validate_entities_journal_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.Journal,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IJournal entity1 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr",
                Number = "1",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 1,
                        Number = "1"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 2,
                        Number = "2"
                    }, 
                }
            };

            IJournal entity2 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 3,
                Source = "scr",
                Number = "3",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 3,
                        Number = "3"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 4,
                        Number = "4"
                    }, 
                }
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate entities journal bad info succeed")]
        public void Validate_entities_journal_bad_info_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 0,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.Journal,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IJournal entity1 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr",
                Number = "1",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 1,
                        Number = "1"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 2,
                        Number = "2"
                    }, 
                }
            };

            IJournal entity2 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 3,
                Source = "scr",
                Number = "3",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 3,
                        Number = "3"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 4,
                        Number = "4"
                    }, 
                }
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate entities journal failed")]
        public void Validate_entities_journal_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.Journal,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IJournal entity1 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr",
                Number = "1",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 1,
                        Number = "1"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 2,
                        Number = "2"
                    }, 
                }
            };

            IJournal entity2 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 3,
                Source = "scr",
                //Number = "3",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 3,
                        Number = "3"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 4,
                        Number = "2"
                    }, 
                }
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeTrue();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate content and type journal succeed")]
        public void Validate_content_and_type_journal_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.Journal,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IJournal entity1 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr",
                Number = "1",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 1,
                        Number = "1"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 2,
                        Number = "2"
                    }, 
                }
            };

            IJournal entity2 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 3,
                Source = "scr",
                Number = "3",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 3,
                        Number = "3"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 4,
                        Number = "4"
                    }, 
                }
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate content and type journal failed")]
        public void Validate_content_and_type_journal_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.Journal,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IJournal entity1 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "scr",
                Number = "1",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 1,
                        Number = "1"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 2,
                        Number = "2"
                    }, 
                }
            };

            IJournal entity2 = new Journal()
            {
                Date = DateTime.Now,
                Type = JournalTypes.Regular,
                Id = Guid.NewGuid(),
                Line = 3,
                Source = "scr",
                Number = "3",
                Lines = new IJournalLine[]
                {
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 3,
                        Number = "3"
                    }, 
                    new JournalLine()
                    {
                        Account = "430",
                        AccountDescription = "desc",
                        Concept = "concept",
                        Credit = 1.0M,
                        Id = Guid.NewGuid(),
                        Line = 4,
                        Number = "4"
                    }, 
                }
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate entities inputInvoice succeed")]
        public void Validate_entities_inputInvoice_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.InputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IInputInvoice entity1 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IInputInvoice entity2 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate entities inputInvoice failed")]
        public void Validate_entities_inputInvoice_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.InputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IInputInvoice entity1 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IInputInvoice entity2 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                //Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeTrue();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate content and type inputInvoice succeed")]
        public void Validate_content_and_type_inputInvoice_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.InputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IInputInvoice entity1 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IInputInvoice entity2 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                //Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate content and type inputInvoice failed")]
        public void Validate_content_and_type_inputInvoice_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.InputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IInputInvoice entity1 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IOutputInvoice entity2 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                //Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate entities outputInvoice succeed")]
        public void Validate_entities_outputInvoice_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.OutputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IOutputInvoice entity1 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IOutputInvoice entity2 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeFalse();
            result.isValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate entities outputInvoice failed")]
        public void Validate_entities_outputInvoice_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.OutputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IOutputInvoice entity1 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IOutputInvoice entity2 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                //Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.IsValid();

            result.errors.Any().Should().BeTrue();
            result.isValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Validate content and type outputInvoice succeed")]
        public void Validate_content_and_type_outputInvoice_succeed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.OutputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IOutputInvoice entity1 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IOutputInvoice entity2 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Validate content and type outputInvoice failed")]
        public void Validate_content_and_type_outputInvoice_failed()
        {
            IMigrationSet set = new MigrationSet()
            {
                Info = new MigrationInfo()
                {
                    Year = 2010,
                    Origin = MigrationOrigin.A3ASESORnom,
                    Type = MigrationType.OutputInvoice,
                    VatNumber = "vatNumber",
                    Version = "2.0"
                },
            };

            IOutputInvoice entity1 = new OutputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IOutputInvoiceLine>()
                {
                    new OutputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            IInputInvoice entity2 = new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    new InputInvoiceLine()
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        BaseAmount = 1210,
                        TaxAmount = 210,
                        Transaction = "OP_INT",
                    }
                }.ToArray(),
                Source = "source"
            };

            set.Entities = new IMigrationEntity[2]{entity1,entity2};

            var result = set.ValidateTypeAndContent();

            result.Should().BeFalse();
        }
    }
}