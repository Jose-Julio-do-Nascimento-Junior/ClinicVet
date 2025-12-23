create or replace PACKAGE BODY PKG_PET AS

 -------------------------------------------------------------------
  -- INSERT_PET
 -------------------------------------------------------------------
  PROCEDURE INSERT_PET(
    P_NAME        IN VARCHAR2,
    P_SPECIES     IN VARCHAR2,
    P_BREED       IN VARCHAR2,
    P_BIRTH_DATE  IN DATE,
    P_OWNER_DOC   IN VARCHAR2,
    P_DOC_TYPE    IN VARCHAR2,
    P_MESSAGE     OUT VARCHAR2
  ) IS
  V_OWNER_ID NUMBER;

  BEGIN
    -- Verifica se o dono existe
    SELECT OWNER_ID 
    INTO V_OWNER_ID
    FROM TBL_PET_OWNER
    WHERE DOCUMENT = P_OWNER_DOC;

    -- Se chegou aqui, dono existe, então insere o pet
    INSERT INTO TBL_PET(NAME, SPECIES, BREED, BIRTH_DATE, OWNER_ID, OWNER_DOCUMENT,DOC_TYPE)
    VALUES (P_NAME, P_SPECIES, P_BREED, P_BIRTH_DATE, V_OWNER_ID, P_OWNER_DOC, P_DOC_TYPE);

    COMMIT;
    P_MESSAGE := 'Pet cadastrado com sucesso.';

    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      -- Nenhum dono encontrado
      P_MESSAGE := 'Dono não encontrado para o documento informado: ' || P_OWNER_DOC;
    WHEN OTHERS THEN
      -- Qualquer outro erro
      P_MESSAGE := 'Erro ao cadastrar pet: ' || SQLERRM;
  END INSERT_PET;

 -------------------------------------------------------------------
  -- UPDATE_PET
 -------------------------------------------------------------------
  PROCEDURE UPDATE_PET(
    P_NAME        IN VARCHAR2,
    P_SPECIES     IN VARCHAR2,
    P_BREED       IN VARCHAR2,
    P_BIRTH_DATE  IN DATE,
    P_OWNER_DOC   IN VARCHAR2,
    P_DOC_TYPE    IN VARCHAR2,
    P_MESSAGE     OUT VARCHAR2
  ) IS
    
  BEGIN
    UPDATE TBL_PET
    SET NAME = NVL(P_NAME, NAME),
      SPECIES = NVL(P_SPECIES, SPECIES),
      BREED = NVL(P_BREED, BREED),
      BIRTH_DATE = NVL(P_BIRTH_DATE, BIRTH_DATE),
      DOC_TYPE = NVL(P_DOC_TYPE, DOC_TYPE)
    WHERE OWNER_DOCUMENT = P_OWNER_DOC
      AND NAME = P_NAME;

    IF SQL%ROWCOUNT = 0 THEN
      P_MESSAGE := 'Nenhum pet encontrado para atualização.';
    ELSE
      P_MESSAGE := 'Pet atualizado com sucesso.';
    END IF;
    EXCEPTION
    WHEN OTHERS THEN
      P_MESSAGE := 'Erro ao atualizar pet: ' || SQLERRM;
  END UPDATE_PET;

 -------------------------------------------------------------------
  -- DELETE_PET
 -------------------------------------------------------------------
  PROCEDURE DELETE_PET(
    P_OWNER_DOC   IN VARCHAR2,
    P_NAME        IN VARCHAR2,
    P_MESSAGE     OUT VARCHAR2
  ) IS
    
  BEGIN
    DELETE FROM TBL_PET
    WHERE OWNER_DOCUMENT = P_OWNER_DOC
    AND NAME = P_NAME;

    IF SQL%ROWCOUNT = 0 THEN
      P_MESSAGE := 'Nenhum pet encontrado para exclusão.';
    ELSE
      P_MESSAGE := 'Pet excluído com sucesso.';
    END IF;
    EXCEPTION
    WHEN OTHERS THEN
      P_MESSAGE := 'Erro ao excluir pet: ' || SQLERRM;
  END DELETE_PET;

 -------------------------------------------------------------------
  -- GET_PETS_BY_OWNER
 -------------------------------------------------------------------
  PROCEDURE GET_PETS_BY_OWNER(
    P_OWNER_DOC IN VARCHAR2,
    P_LIMIT      IN NUMBER,
    P_OFFSET     IN NUMBER,
    V_CURSOR     OUT SYS_REFCURSOR
  ) IS
   
  BEGIN
    OPEN V_CURSOR FOR
      SELECT 
        o.NAME AS NOME_TUTOR,
        o.DOCUMENT AS DOC_TUTOR,
        o.DOCUMENT_TYPE AS TIPO_DOCUMENTO,
        p.NAME AS NOME_PET,
        p.SPECIES AS ESPECIE,
        p.BREED AS RACA,
        TO_CHAR(p.BIRTH_DATE, 'DD/MM/YYYY') AS DATA_NASCIMENTO
      FROM 
        TBL_PET_OWNER o
      JOIN 
        TBL_PET p 
      ON p.OWNER_ID = o.OWNER_ID
      WHERE 
        P_OWNER_DOC IS NULL OR o.DOCUMENT = P_OWNER_DOC
      ORDER BY 
        p.NAME
        OFFSET NVL(P_OFFSET, 0) ROWS
    FETCH NEXT NVL(P_LIMIT, 10) ROWS ONLY;
  END GET_PETS_BY_OWNER;
END PKG_PET;