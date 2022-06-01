namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;


    public class ActivityValidation : Validation<IActivity>
    {
        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Id'"));

            this.CreateRule(x => this.Validate(x.Description),
                this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción'"));
            this.CreateRule(x => this.Validate(x.ShortDescription, 20),
                this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción corta'"));
            this.CreateRule(x => this.Validate(x.Taxation),
                this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tributación'"));

            this.CreateRule(x => this.Validate(x.Estimations), "'Epígrafes inválidos'"); 
        }

        private bool Validate(Taxation taxation)
        {
            return taxation == Taxation.State || taxation == Taxation.CanaryIsland;
        }


        private bool Validate(IEstimation[] estimations)
        {
            if (estimations == null)
            {
                return true;
            }

            foreach (var estimation in estimations)
            {
                if (estimation == null)
                {
                    return false;
                }

                bool isValid = Validate(estimation);
                if (!isValid)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Validate(IEstimation estimation)
        {
            return estimation.Year >= 2000 && estimation.Year <= 2050 &&
                   !string.IsNullOrWhiteSpace(estimation.Epigraph);
        }
    }
}
