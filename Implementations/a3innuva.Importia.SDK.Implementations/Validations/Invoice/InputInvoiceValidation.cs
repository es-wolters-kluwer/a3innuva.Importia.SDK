namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class InputInvoiceValidation : Validation<IInputInvoice>
    {
        private readonly IValidation<IInputInvoiceLine> lineValidation;
        private readonly IValidation<IPayment> paymentValidation;
        private readonly Regex accountCodeFormat;
        private readonly Regex nifFormat;
        private readonly Regex postalCodeFormat;

        public InputInvoiceValidation()
        {
            this.lineValidation = new InputInvoiceLineValidation();
            this.paymentValidation = new PaymentValidation();
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$");
            this.nifFormat = new Regex(@"^[A-Z0-9]*$");
            this.postalCodeFormat = new Regex(@"^[0-9]{5}$");
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.PartnerAccount), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta de cliente'"));
            this.CreateRule(x => this.Validate(x.PartnerAccount, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de cliente'"));
            this.CreateRule(x => this.accountCodeFormat.IsMatch(x.PartnerAccount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de cliente'"));

            this.CreateRule(x => this.Validate(x.InvoiceDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de factura'"));
            this.CreateRule(x => this.Validate(x.JournalDate), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Fecha de asiento'"));
            this.CreateRule(x => this.Validate(x.TransactionDate), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Fecha de operación'"));

            this.CreateRule(x => this.Validate(x.InvoiceNumber), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Número de factura'"));
            this.CreateRule(x => this.Validate(x.InvoiceNumber, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de factura'"));

            this.CreateRule(x => this.ValidateNullable(x.VatNumber, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'NIF'"));
            this.CreateRule(x => this.ValidateVatNumber(x.VatNumber), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'NIF'"));

            this.CreateRule(x => this.ValidateNullable(x.PartnerName, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Nombre de cliente'"));
            this.CreateRule(x => this.Validate(x.Source), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Origen'"));

            this.CreateRule(x => this.ValidateVatType(x.VatType), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de documento'"));
            this.CreateRule(x => this.ValidatePostalCode(x.PostalCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código postal'"));
            this.CreateRule(x => this.ValidateNullable(x.CountryCode, 2), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Código país'"));

            this.CreateRule(x => this.ValidateAmounts(x.PendingAmount, x.SatisfiedAmount), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Solo un importe informado'"));
        }

        public override IEnumerable<IValidationResult> Validate(IInputInvoice entity)
        {
            List<IValidationResult> errors = new List<IValidationResult>();

            foreach (var item in entity.Lines)
            {
                var result = lineValidation.Validate(item);

                var resultErrors = result.Where(x => x.IsValid == false);
                errors.AddRange(resultErrors);
            }

            if (entity.Maturities != null)
            {
                foreach (var item in entity.Maturities)
                {
                    var result = paymentValidation.Validate(item);

                    var resultErrors = result.Where(x => x.IsValid == false);
                    errors.AddRange(resultErrors);
                }
            }

            var entityErrors = base.Validate(entity);

            errors.AddRange(entityErrors);
            return errors;
        }

        private bool ValidateVatNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.nifFormat.IsMatch(input);
        }

        private bool ValidateVatType(int input)
        {
            return input >= 0 && input <= 8;
        }

        private bool ValidatePostalCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.postalCodeFormat.IsMatch(input);
        }

        private bool ValidateAmounts(decimal? pendingAmount, decimal? satisfiedAmount)
        {
            if (!pendingAmount.HasValue && !satisfiedAmount.HasValue)
                return true;

            if (pendingAmount.HasValue && !satisfiedAmount.HasValue)
                return false;

            if (!pendingAmount.HasValue)
                return false;

            return true;
        }
    }
}
