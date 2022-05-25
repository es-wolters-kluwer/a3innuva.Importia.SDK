namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;


    public class ChannelValidation : Validation<IChannel>
    {
        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Id'"));

            this.CreateRule(x => this.Validate(x.Description),
                this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción'"));
            this.CreateRule(x => this.Validate(x.ShortDescription, 20),
                this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción corta'"));
        }
    }
}
