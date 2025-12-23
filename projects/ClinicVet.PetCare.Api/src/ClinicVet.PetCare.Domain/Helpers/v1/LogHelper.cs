using ClinicVet.PetCare.Domain.Resources.v1;
using System.Text;

namespace ClinicVet.PetCare.Domain.Helper.v1;

public static class LogHelper
{
    public static string GetLogIdentifier(string repositoryName, object filter, string procedure)
    {
        var sb = new StringBuilder();
        sb.Append(repositoryName + Constants.HyphenSpace + Constants.Procedure
                  + Constants.ColonSpace + procedure
                  + Constants.HyphenSpace + Constants.Parameters + Constants.ColonSpace);

        foreach (var prop in filter.GetType().GetProperties())
            sb.Append(Constants.HyphenSpace + prop.Name + Constants.ColonSpace + prop.GetValue(filter, null));

        return sb.ToString();
    }
}