create or replace PACKAGE BODY PKG_AGENDA AS
 -------------------------------------------------------------------------------
  -- INSERT_AGENDA
 -------------------------------------------------------------------------------
  PROCEDURE INSERT_AGENDA(
    P_APPOINTMENT_AT IN DATE,
    P_REASON         IN VARCHAR2,
    P_STATUS         IN VARCHAR2,
    P_PET_NAME       IN VARCHAR2,
    P_PET_SPECIES    IN VARCHAR2,
    P_OWNER_DOCUMENT IN VARCHAR2,
    P_OWNER_PHONE    IN VARCHAR2,
    P_OWNER_TYPE     IN VARCHAR2,
    P_DOC_TYPE       IN VARCHAR2,
    P_MESSAGE        OUT VARCHAR2
  ) IS
  V_OWNER_ID NUMBER;
  V_PET_ID   NUMBER;
  
  BEGIN
    -- Verifica se o tutor existe
    SELECT OWNER_ID INTO V_OWNER_ID
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_OWNER_DOCUMENT;

    -- Verifica se o pet existe para este tutor
    SELECT PET_ID INTO V_PET_ID
    FROM TBL_PET
    WHERE NAME = P_PET_NAME
      AND OWNER_ID = V_OWNER_ID;

    -- Insere o agendamento
    INSERT INTO TBL_AGENDA(
      APPOINTMENT_AT,
      REASON,
      STATUS,
      PET_ID,
      OWNER_ID,
      PET_NAME,
      PET_SPECIES,
      OWNER_PHONE,
      OWNER_TYPE,
      OWNER_DOCUMENT,
      DOC_TYPE
    )
    VALUES (
      P_APPOINTMENT_AT,
      P_REASON,
      NVL(P_STATUS, 'AGENDADO'),
      V_PET_ID,
      V_OWNER_ID,
      P_PET_NAME,
      P_PET_SPECIES,
      P_OWNER_PHONE,
      P_OWNER_TYPE,
      P_OWNER_DOCUMENT,
      P_DOC_TYPE
    );

    P_MESSAGE := 'Agendamento cadastrado com sucesso.';
    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      P_MESSAGE := 'Tutor ou pet não encontrado para os dados informados.';
    WHEN OTHERS THEN
      P_MESSAGE := 'Erro ao cadastrar agendamento: ' || SQLERRM;
  END INSERT_AGENDA;
  
 -------------------------------------------------------------------------------
  -- UPDATE_AGENDA
 -------------------------------------------------------------------------------
  PROCEDURE UPDATE_AGENDA(
    P_APPOINTMENT_AT IN DATE,
    P_REASON         IN VARCHAR2,
    P_STATUS         IN VARCHAR2,
    P_PET_NAME       IN VARCHAR2,
    P_PET_SPECIES    IN VARCHAR2,
    P_OWNER_DOCUMENT IN VARCHAR2,
    P_OWNER_PHONE    IN VARCHAR2,
    P_OWNER_TYPE     IN VARCHAR2,
    P_DOC_TYPE       IN VARCHAR2,
    P_MESSAGE        OUT VARCHAR2
  ) IS
  V_OWNER_ID NUMBER;
  V_PET_ID   NUMBER;
  
  BEGIN
    -- Obtém IDs do tutor e pet
    SELECT OWNER_ID INTO V_OWNER_ID
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_OWNER_DOCUMENT;

    SELECT PET_ID INTO V_PET_ID
    FROM TBL_PET
    WHERE NAME = P_PET_NAME
      AND OWNER_ID = V_OWNER_ID;

    -- Atualiza agendamento
    UPDATE TBL_AGENDA
      SET APPOINTMENT_AT = NVL(P_APPOINTMENT_AT, APPOINTMENT_AT),
      REASON         = NVL(P_REASON, REASON),
      STATUS         = NVL(P_STATUS, STATUS),
      PET_NAME       = NVL(P_PET_NAME, PET_NAME),
      PET_SPECIES    = NVL(P_PET_SPECIES, PET_SPECIES),
      OWNER_PHONE    = NVL(P_OWNER_PHONE, OWNER_PHONE),
      OWNER_TYPE     = NVL(P_OWNER_TYPE, OWNER_TYPE),
      DOC_TYPE       = NVL(P_DOC_TYPE, DOC_TYPE)
    WHERE PET_ID = V_PET_ID
      AND OWNER_ID = V_OWNER_ID;

    IF SQL%ROWCOUNT = 0 THEN
      P_MESSAGE := 'Nenhum agendamento encontrado para atualização.';
    ELSE
      P_MESSAGE := 'Agendamento atualizado com sucesso.';
    END IF;
    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      P_MESSAGE := 'Tutor ou pet não encontrado para os dados informados.';
    WHEN OTHERS THEN
      P_MESSAGE := 'Erro ao atualizar agendamento: ' || SQLERRM;
  END UPDATE_AGENDA;
  
 -------------------------------------------------------------------------------
  -- DELETE_AGENDA
 -------------------------------------------------------------------------------
  PROCEDURE DELETE_AGENDA(
    P_APPOINTMENT_AT IN DATE,
    P_PET_NAME       IN VARCHAR2,
    P_OWNER_DOCUMENT IN VARCHAR2,
    P_MESSAGE        OUT VARCHAR2
  ) IS
  V_OWNER_ID NUMBER;
  V_PET_ID   NUMBER;
  
  BEGIN
    -- Obtém IDs do tutor e pet
    SELECT OWNER_ID INTO V_OWNER_ID
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_OWNER_DOCUMENT;

    SELECT PET_ID INTO V_PET_ID
    FROM TBL_PET
    WHERE NAME = P_PET_NAME
      AND OWNER_ID = V_OWNER_ID;

    -- Deleta agendamento
    DELETE FROM TBL_AGENDA
    WHERE PET_ID = V_PET_ID
      AND OWNER_ID = V_OWNER_ID
      AND APPOINTMENT_AT = P_APPOINTMENT_AT;

    IF SQL%ROWCOUNT = 0 THEN
      P_MESSAGE := 'Nenhum agendamento encontrado para exclusão.';
    ELSE
      P_MESSAGE := 'Agendamento excluído com sucesso.';
    END IF;
    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      P_MESSAGE := 'Tutor ou pet não encontrado para os dados informados.';
    WHEN OTHERS THEN
      P_MESSAGE := 'Erro ao excluir agendamento: ' || SQLERRM;
  END DELETE_AGENDA;
  
 -------------------------------------------------------------------------------
  -- GET_AGENDAS
 -------------------------------------------------------------------------------
  PROCEDURE GET_AGENDAS(
    P_OWNER_DOC IN VARCHAR2,
    P_STATUS    IN VARCHAR2,
    P_LIMIT     IN NUMBER,
    P_OFFSET    IN NUMBER,
    V_CURSOR    OUT SYS_REFCURSOR
  ) IS
  
  BEGIN
    OPEN V_CURSOR FOR
    SELECT 
      TO_CHAR(a.APPOINTMENT_AT, 'DD/MM/YYYY HH24:MI') AS CONSULTA_AGENDADA,
      a.REASON AS MOTIVO_CONSULTA,
      a.STATUS AS STATUS_CONSULTA,
      a.PET_NAME AS NOME_PET,
      a.PET_SPECIES AS ESPECIE_PET,
      a.OWNER_PHONE AS CONTATO_TUTOR,
      a.OWNER_TYPE AS TIPO_TUTOR ,
      a.OWNER_DOCUMENT AS DOCUMENTO_TUTOR,
      a.DOC_TYPE AS TIPO_DOCUMENTO
    FROM 
      TBL_AGENDA a
    JOIN 
      TBL_PET_OWNER o
      ON a.OWNER_ID = o.OWNER_ID
    WHERE 
      (P_OWNER_DOC IS NULL OR o.DOCUMENT = P_OWNER_DOC)
      AND  
      (P_STATUS IS NULL OR a.STATUS = P_STATUS) 
    ORDER BY 
      a.APPOINTMENT_AT
      OFFSET NVL(P_OFFSET, 0) ROWS
    FETCH NEXT NVL(P_LIMIT, 10) ROWS ONLY;
  END GET_AGENDAS;
END PKG_AGENDA;