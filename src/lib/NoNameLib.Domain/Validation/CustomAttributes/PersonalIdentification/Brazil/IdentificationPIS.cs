namespace NoNameLib.Domain.Validation.PersonalIdentification.Brazil
{
    public class IdentificationPIS : IdentificationType
    {
        public IdentificationPIS(
            string identifier) : base(identifier) { }

        public override bool IsValid()
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (_identifier.Trim().Length != 11)
                return false;
            var pis = _identifier.Replace("-", "").Replace(".", "").PadLeft(11, '0');
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }
    }
}
