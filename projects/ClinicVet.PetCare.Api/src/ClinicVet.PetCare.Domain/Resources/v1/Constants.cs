namespace ClinicVet.PetCare.Domain.Resources.v1;

public static class Constants
{
    #region Configuration_Parameters
    public const string Procedure = "Procedure";
    public const string HyphenSpace = " - ";
    public const string ColonSpace = " : ";
    public const string Parameters = "Parameters";
    public const string OracleSkipInput = "P_LIMIT";
    public const string OracleTakeInput = "P_OFFSET";
    public const string Language = "pt_BR";
    public const string OracleMessageOutput = "P_MESSAGE";
    #endregion

    #region Output_Owner
    public const string OracleNameColumn = "NOME";
    public const string OracleDocumentColumn = "DOCUMENTO";
    public const string OracleDocumentTypeColumn = "TIPO_DOCUMENTO";
    public const string OracleOwnerTypeColumn = "TIPO_TUTOR";
    public const string OraclePhoneColumn = "CONTATO";
    public const string OracleCreatedAtColumn = "DATA_CADASTRO";
    public const string OracleStreetColumn = "RUA";
    public const string OracleNumberColumn = "NUMERO";
    public const string OracleCityColumn = "CIDADE";
    public const string OracleStateColumn = "ESTADO";
    public const string OracleZipCodeColumn = "CEP";
    public const string OracleRefCursorColumn = "V_CURSOR";
    #endregion

    #region Input_Owner
    public const string OracleNameInput = "P_NAME";
    public const string OracleDocumentInput = "P_DOCUMENT";
    public const string OracleDocumentTypeInput = "P_DOCUMENT_TYPE";
    public const string OracleOwnerTypeInput = "P_OWNER_TYPE";
    public const string OraclePhoneInput = "P_PHONE";
    public const string OracleStreetInput = "P_STREET";
    public const string OracleNumberInput = "P_RESIDENCE_NUMBER";
    public const string OracleCityInput = "P_CITY";
    public const string OracleStateInput = "P_STATE";
    public const string OracleZipCodeInput = "P_ZIP_CODE";
    public const string OracleDocumentFilterInput = "P_DOCUMENT_FILTER";
    #endregion

    #region Output_Pet
    public const string OracleOwnerNameColumn = "NOME_TUTOR";
    public const string OracleOwnerDocumentColumn = "DOC_TUTOR";
    public const string OraclePetNameColumn = "NOME_PET";
    public const string OracleSpecieColumn = "ESPECIE";
    public const string OracleBreedColumn = "RACA";
    public const string OracleBirthDateColumn = "DATA_NASCIMENTO";
    public const string OracleDocTypeColumn = "TIPO_DOCUMENTO";
    #endregion

    #region Input_Pet
    public const string OracleOwnerDocInput = "P_OWNER_DOC";
    public const string OracleSpeciesInput = "P_SPECIES";
    public const string OracleBreedInput = "P_BREED";
    public const string OracleBirthDateInput = "P_BIRTH_DATE";
    public const string OracleDocTypeInput = "P_DOC_TYPE";
    #endregion

    #region Output_Agenda
    public const string OracleAppointmentAtColumn = "CONSULTA_AGENDADA";
    public const string OracleRasonColumn = "MOTIVO_CONSULTA";
    public const string OracleAgendaStatusTypeColumn = "STATUS_CONSULTA";
    public const string OraclePetAgendaColumn = "NOME_PET";
    public const string OracleSpeciePetAgendaColumn = "ESPECIE_PET";
    public const string OraclePhoneOwnerAgendaColumn = "CONTATO_TUTOR";
    public const string OracleOwnerTypeAgendaColumn = "TIPO_TUTOR";
    public const string OracleDocumentOwnerAgendaColumn = "DOCUMENTO_TUTOR";
    public const string OracleDocumentTypeAgendaColumn = "TIPO_DOCUMENTO";
    #endregion

    #region Input_Agenda
    public const string OracleAppointmentAtInput = "P_APPOINTMENT_AT";
    public const string OracleRasonInput = "P_REASON";
    public const string OracleStatusInput = "P_STATUS";
    public const string OraclePetAgendaInput = "P_PET_NAME";
    public const string OracleSpeciePetAgendaInput = "P_PET_SPECIES";
    public const string OracleDocumentOwnerAgenda = "P_OWNER_DOCUMENT";
    public const string OraclePhoneOwnerAgendaInput = "P_OWNER_PHONE";
    public const string OracleOwnerTypeAgendaInput = "P_OWNER_TYPE";
    public const string OracleDocumentTypeAgendaInput = "P_DOC_TYPE";
    #endregion
}