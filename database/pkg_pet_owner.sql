create or replace PACKAGE BODY PKG_PET_OWNER AS

 -------------------------------------------------------------------
  -- INSERT_PET_OWNER
 -------------------------------------------------------------------
  PROCEDURE INSERT_PET_OWNER(
    P_NAME             IN VARCHAR2,
    P_DOCUMENT         IN VARCHAR2,
    P_DOCUMENT_TYPE    IN VARCHAR2,
    P_OWNER_TYPE       IN VARCHAR2,
    P_PHONE            IN VARCHAR2,
    P_STREET           IN VARCHAR2,
    P_RESIDENCE_NUMBER IN VARCHAR2,
    P_CITY             IN VARCHAR2,
    P_STATE            IN VARCHAR2,
    P_ZIP_CODE         IN VARCHAR2,
    P_MESSAGE          OUT VARCHAR2
  ) IS
  V_ADDRESS_ID NUMBER;
  V_COUNT      NUMBER;
    
  BEGIN
   -- Verifica se já existe registro com mesmo documento
    SELECT COUNT(1)
    INTO V_COUNT
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_DOCUMENT;
     
    IF V_COUNT > 0 THEN
      P_MESSAGE := 'Documento já cadastrado:' || P_DOCUMENT;
       -- Aborta sem inserir
      RETURN;
    END IF;
    
    -- Insert Address
    INSERT INTO TBL_ADDRESS (
      STREET, RESIDENCE_NUMBER, CITY, STATE, ZIP_CODE
    )
    VALUES (
      P_STREET, P_RESIDENCE_NUMBER, P_CITY, P_STATE, P_ZIP_CODE
    )
    RETURNING ADDRESS_ID INTO V_ADDRESS_ID;

    -- Insert PetOwner
    INSERT INTO TBL_PET_OWNER (
      NAME, DOCUMENT, DOCUMENT_TYPE, OWNER_TYPE, PHONE, ADDRESS_ID, CREATED_AT
    )
    VALUES (
      P_NAME, P_DOCUMENT, P_DOCUMENT_TYPE, P_OWNER_TYPE, P_PHONE, V_ADDRESS_ID, SYSDATE
    );
      
    P_MESSAGE := 'Usuário cadastrado com sucesso.';
       
    EXCEPTION
    WHEN OTHERS THEN
    P_MESSAGE := 'Erro ao Cadastrar: ';
    RAISE;
  END INSERT_PET_OWNER;

 -------------------------------------------------------------------
  -- UPDATE_PET_OWNER
 -------------------------------------------------------------------
  PROCEDURE UPDATE_PET_OWNER(
    P_NAME             IN VARCHAR2,
    P_DOCUMENT         IN VARCHAR2,
    P_DOCUMENT_TYPE    IN VARCHAR2,
    P_OWNER_TYPE       IN VARCHAR2,
    P_PHONE            IN VARCHAR2,
    P_STREET           IN VARCHAR2,
    P_RESIDENCE_NUMBER IN VARCHAR2,
    P_CITY             IN VARCHAR2,
    P_STATE            IN VARCHAR2,
    P_ZIP_CODE         IN VARCHAR2,
    P_MESSAGE          OUT VARCHAR2
  ) IS
  V_ADDRESS_ID  NUMBER;
  V_COUNT_DUP   NUMBER;
  V_OWNER_ID    NUMBER;
    
  BEGIN
    -- Verifica se o documento de filtro existe e pega o ADDRESS_ID
    SELECT ADDRESS_ID
    INTO V_ADDRESS_ID
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_DOCUMENT;

    -- Verifica se o novo documento já existe em outro dono (evita duplicidade)
    SELECT COUNT(1)
    INTO V_COUNT_DUP
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_DOCUMENT
    AND OWNER_ID <> V_OWNER_ID;
       
    IF V_COUNT_DUP > 0 THEN
      P_MESSAGE := 'Documento já cadastrado em outro dono: ' || P_DOCUMENT;
      RETURN;
    END IF;

    -- Atualiza endereço
    UPDATE TBL_ADDRESS
      SET STREET = NVL(P_STREET, STREET),
      RESIDENCE_NUMBER = NVL(P_RESIDENCE_NUMBER, RESIDENCE_NUMBER),
      CITY = NVL(P_CITY, CITY),
      STATE = NVL(P_STATE, STATE),
      ZIP_CODE = NVL(P_ZIP_CODE, ZIP_CODE)
    WHERE ADDRESS_ID = V_ADDRESS_ID;
     
    -- Atualiza dono (pelo documento de filtro)
    UPDATE TBL_PET_OWNER
      SET NAME = NVL(P_NAME, NAME),
      DOCUMENT = NVL(P_DOCUMENT, DOCUMENT),
      DOCUMENT_TYPE = NVL(P_DOCUMENT_TYPE, DOCUMENT_TYPE),
      OWNER_TYPE = NVL(P_OWNER_TYPE, OWNER_TYPE),
      PHONE = NVL(P_PHONE, PHONE)
    WHERE DOCUMENT = P_DOCUMENT;
     
    -- Verifica se o update realmente afetou algum registro
    IF SQL%ROWCOUNT = 0 THEN
      P_MESSAGE := 'Nenhum registro encontrado para o documento informado: ' || P_DOCUMENT;
      RETURN;
    END IF;
    
    -- Mensagem de sucesso
    P_MESSAGE := 'Atualização feita com sucesso.';
    
    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      P_MESSAGE := 'Nenhum registro encontrado com o documento informado: ' || P_DOCUMENT;
    WHEN OTHERS THEN
      P_MESSAGE := 'A atualização falhou: ' || SQLERRM;
  END UPDATE_PET_OWNER;

 -------------------------------------------------------------------
  -- DELETE_PET_OWNER
 -------------------------------------------------------------------
  PROCEDURE DELETE_PET_OWNER(
    P_OWNER_ID IN NUMBER
  ) IS
  V_ADDRESS_ID NUMBER;
   
  BEGIN
    -- Get address ID
    SELECT ADDRESS_ID INTO V_ADDRESS_ID
    FROM TBL_PET_OWNER
    WHERE OWNER_ID = P_OWNER_ID;

    -- Delete owner
    DELETE FROM TBL_PET_OWNER
    WHERE OWNER_ID = P_OWNER_ID;

    -- Delete address
    DELETE FROM TBL_ADDRESS
    WHERE ADDRESS_ID = V_ADDRESS_ID;
  END DELETE_PET_OWNER;

 -------------------------------------------------------------------
  -- GET_PET_OWNERS
 -------------------------------------------------------------------
  PROCEDURE GET_PET_OWNERS(
    P_NAME     IN VARCHAR2 DEFAULT NULL,
    P_DOCUMENT IN VARCHAR2 DEFAULT NULL,
    P_LIMIT    IN NUMBER DEFAULT 10,
    P_OFFSET   IN NUMBER DEFAULT 0,
    V_CURSOR   OUT t_ref_cursor
  ) IS
   
  BEGIN
    OPEN V_CURSOR FOR
      SELECT 
        o.NAME AS NOME,
        o.DOCUMENT AS DOCUMENTO,
        o.DOCUMENT_TYPE  AS TIPO_DOCUMENTO,
        o.OWNER_TYPE AS TIPO_TUTOR,
        o.PHONE AS CONTATO,
        TO_CHAR(o.CREATED_AT, 'DD/MM/YYYY') AS DATA_CADASTRO,
        a.STREET AS RUA,
        a.RESIDENCE_NUMBER AS NUMERO,
        a.CITY AS CIDADE,
        a.STATE AS ESTADO,
        a.ZIP_CODE AS CEP
      FROM TBL_PET_OWNER o
      JOIN TBL_ADDRESS a ON o.ADDRESS_ID = a.ADDRESS_ID
      WHERE (P_NAME IS NULL OR UPPER(o.NAME) LIKE '%' || UPPER(P_NAME) || '%')
        AND (P_DOCUMENT IS NULL OR o.DOCUMENT LIKE '%' || P_DOCUMENT || '%')
    ORDER BY o.OWNER_ID
    OFFSET P_OFFSET ROWS FETCH NEXT P_LIMIT ROWS ONLY;
  END GET_PET_OWNERS;
END PKG_PET_OWNER;