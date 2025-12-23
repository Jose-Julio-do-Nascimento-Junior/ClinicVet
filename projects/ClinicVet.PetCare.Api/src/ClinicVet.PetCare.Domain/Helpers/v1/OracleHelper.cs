using ClinicVet.PetCare.Domain.Resources.v1;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace ClinicVet.PetCare.Domain.Helper.v1;

public static class OracleHelper
{
    public static void GetBaseOracleParameters(ref List<OracleParameter> parameters, int take, int? skip)
    {
        parameters.Add(new OracleParameter { ParameterName = Constants.OracleSkipInput, Value = take, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input });
        parameters.Add(new OracleParameter { ParameterName = Constants.OracleTakeInput, Value = skip, OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input });
        parameters.Add(new OracleParameter { ParameterName = Constants.OracleRefCursorColumn, OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output });
    }

    public static T? GetValueFromReader<T>(DbDataReader reader, string columnName)
    {
        var index = reader.GetOrdinal(columnName);
        return !reader.IsDBNull(index) ? reader.GetFieldValue<T>(index) : default(T);
    }

    public static T? GetEnumFromReader<T>(DbDataReader reader, string columnName) where T : struct, Enum
    {
        var stringValue = GetValueFromReader<string>(reader, columnName);

        if (!string.IsNullOrEmpty(stringValue) && Enum.TryParse<T>(stringValue, ignoreCase: true, out var parsed))
        {
            return parsed;
        }

        return null;
    }
}