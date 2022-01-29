USE MyCompanyTest
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

If Object_Id('dbo.AtualizaFaturadoProduto') is not null
begin
   drop procedure dbo.AtualizaFaturadoProduto
end
go

CREATE PROCEDURE AtualizaFaturadoProduto
AS
BEGIN
	--delete from dbo.FaturadoProduto

	IF OBJECT_ID('tempdb..#ADDFAT', 'U') IS NOT NULL DROP TABLE #BO_MES

	CREATE TABLE #ADDFAT 
	(
		[Descricao] [nvarchar](255) NULL,
		[Quantidade] [float] NULL,
		[Consultas] [float] NULL,
		[PrcUnitario] [float] NULL,
		[Total] [float] NULL,
		[Nota] [nvarchar](10) NULL,
		[CNPJCPF] [bigint] NULL,
		[NomeCliente] [nvarchar](255) NULL,
		[NumPedido] [nvarchar](255) NULL,
		[DataEmissao] [datetime] NULL,
		[Vendedor] [nvarchar](255) NULL,
		[CentroCusto] [nvarchar](255) NULL,
		[Vencimento] [datetime] NULL,
		[DtBaixa] [date] NULL,
		[Oportunidade] [nvarchar](255) NULL,
		[MesReferencia] [nvarchar](7) NULL,
		[Email] [nvarchar](255) NULL,
		[Observacao] [nvarchar](255) NULL,
		[StatusFaturados] [int] NULL,
		[Existe] nvarchar(10) NULL
	)

	-- Seleciona os registros que serão incluidos
	INSERT INTO #ADDFAT
	(
		[Descricao]
		,[Quantidade]
		,[Consultas]
		,[PrcUnitario]
		,[Total]
		,[Nota]
		,[CNPJCPF]
		,[NomeCliente]
		,[NumPedido]
		,[DataEmissao]
		,[Vendedor]
		,[CentroCusto]
		,[Vencimento]
		,[DtBaixa]
		,[Oportunidade]
		,[MesReferencia]
		,[Email]
		,[Observacao]
		,[StatusFaturados]
		,Existe
	)
	SELECT	TmpFaturadoProduto.[Descricao]
			,TmpFaturadoProduto.[Quantidade]
			,TmpFaturadoProduto.[Consultas]
			,TmpFaturadoProduto.[Prc Unitario]
			,TmpFaturadoProduto.[Total]
			,TmpFaturadoProduto.[Nota]
			,CONVERT(BIGINT, TmpFaturadoProduto.[CNPJ/CPF]) 
			,TmpFaturadoProduto.[Nome Cliente]
			,TmpFaturadoProduto.[Num# Pedido]
			,TmpFaturadoProduto.[Data Emissao]
			,TmpFaturadoProduto.[Vendedor]
			,TmpFaturadoProduto.[Centro de Custo]
			,TmpFaturadoProduto.[Vencimento]
			,TmpFaturadoProduto.[Dt Baixa]
			,TmpFaturadoProduto.[Oportunidade]
			,TmpFaturadoProduto.[Mes Referencia]
			,TmpFaturadoProduto.[E-mail]
			,TmpFaturadoProduto.[Observacao]
			,1 StatusFaturados     -- Nova inclusão
			,FaturadoProduto.Nota
	FROM	TmpFaturadoProduto LEFT OUTER JOIN FaturadoProduto
			ON TmpFaturadoProduto.Nota = FaturadoProduto.Nota  
			--ON	TmpFaturadoProduto.[CNPJ/CPF] = FaturadoProduto.CNPJCPF
			--AND TmpFaturadoProduto.Nota = FaturadoProduto.Nota  
	WHERE	FaturadoProduto.Nota is NULL

	Select 'ADD' ParaAdicionar, * from #ADDFAT

	---- Inclusão de novos Clientes X Produtos
	INSERT INTO dbo.FaturadoProduto
	(
			[Descricao]
			,[Quantidade]
			,[Consultas]
			,[PrcUnitario]
			,[Total]
			,[Nota]
			,[CNPJCPF]
			,[NomeCliente]
			,[NumPedido]
			,[DataEmissao]
			,[Vendedor]
			,[CentroCusto]
			,[Vencimento]
			,[DtBaixa]
			,[Oportunidade]
			,[MesReferencia]
			,[Email]
			,[Observacao]
			,[StatusFaturados]
	)
	SELECT
		[Descricao]
		,[Quantidade]
		,[Consultas]
		,[PrcUnitario]
		,[Total]
		,[Nota]
		,[CNPJCPF]
		,[NomeCliente]
		,[NumPedido]
		,[DataEmissao]
		,[Vendedor]
		,[CentroCusto]
		,[Vencimento]
		,[DtBaixa]
		,[Oportunidade]
		,[MesReferencia]
		,[Email]
		,[Observacao]
		,[StatusFaturados]
	FROM #ADDFAT

	-- Update data da Baixa
	--delete from #ADDFAT

	--INSERT INTO #ADDFAT
	--(
	--	[Descricao]
	--	,[Quantidade]
	--	,[Consultas]
	--	,[PrcUnitario]
	--	,[Total]
	--	,[Nota]
	--	,[CNPJCPF]
	--	,[NomeCliente]
	--	,[NumPedido]
	--	,[DataEmissao]
	--	,[Vendedor]
	--	,[CentroCusto]
	--	,[Vencimento]
	--	,[DtBaixa]
	--	,[Oportunidade]
	--	,[MesReferencia]
	--	,[Email]
	--	,[Observacao]
	--	,[StatusFaturados]
	--)
	--SELECT	TmpFaturadoProduto.[Descricao]
	--		,TmpFaturadoProduto.[Quantidade]
	--		,TmpFaturadoProduto.[Consultas]
	--		,TmpFaturadoProduto.[Prc Unitario]
	--		,TmpFaturadoProduto.[Total]
	--		,TmpFaturadoProduto.[Nota]
	--		,CONVERT(BIGINT, TmpFaturadoProduto.[CNPJ/CPF]) 
	--		,TmpFaturadoProduto.[Nome Cliente]
	--		,TmpFaturadoProduto.[Num# Pedido]
	--		,TmpFaturadoProduto.[Data Emissao]
	--		,TmpFaturadoProduto.[Vendedor]
	--		,TmpFaturadoProduto.[Centro de Custo]
	--		,TmpFaturadoProduto.[Vencimento]
	--		,TmpFaturadoProduto.[Dt Baixa]
	--		,TmpFaturadoProduto.[Oportunidade]
	--		,TmpFaturadoProduto.[Mes Referencia]
	--		,TmpFaturadoProduto.[E-mail]
	--		,TmpFaturadoProduto.[Observacao]
	--		,1 StatusFaturados     -- Nova inclusão
	--FROM	TmpFaturadoProduto INNER JOIN FaturadoProduto
	--		ON TmpFaturadoProduto.Nota = FaturadoProduto.Nota  
	--		--ON	TmpFaturadoProduto.[CNPJ/CPF] = FaturadoProduto.CNPJCPF
	--		--AND TmpFaturadoProduto.Nota = FaturadoProduto.Nota
	--Where	TmpFaturadoProduto.[Dt Baixa] is not null
	
	--Select 'UPD', * from #ADDFAT
		
	--UPDATE	FaturadoProduto
	--SET		DtBaixa = #ADDFAT.DtBaixa 
	--FROM	FaturadoProduto INNER JOIN #ADDFAT
	--		ON FaturadoProduto.Nota = #ADDFAT.Nota

	--Select * from #ADDFAT
	Select 'Teste', * from FaturadoProduto
END








/*











    IF OBJECT_ID('tempdb..#BO_MES', 'U') IS NOT NULL 
		DROP TABLE #BO_MES

	CREATE TABLE #BO_MES
	(  
		[NR_CNPJ] bigint
		,[NM_TP_TARIFACAO] nvarchar(50)
        ,CC nvarchar(50)
        ,SERVERID bigint
		,[ANO_CONSULTA] int
		,[MES_CONSULTA] Int
		,[QT_UTILIZA] int
        ,Flag nvarchar(1)
        ,NrRegistros Int
	)

	-- Obter todos os registros de Consumo Mensal - sumarizados
    INSERT INTO #BO_MES (
		[NR_CNPJ],
		[NM_TP_TARIFACAO],
        CC,
        SERVERID,
		[ANO_CONSULTA],
		[MES_CONSULTA],
		[QT_UTILIZA],
        Flag,
        NrRegistros
        )
	SELECT	
        Cnpj
        ,TipoTarifacao
        ,CC
        ,ServerId
        ,Year(DataUtilizacao)
        ,Month(DataUtilizacao)
        ,sum(QuantidadeUtilizacao)
        ,1
        ,COUNT(DataUtilizacao)
	FROM	
        ConsumoDiario
    Where StatusDiario <> 1
	GROUP BY 
        Cnpj
        ,TipoTarifacao
        ,CC
        ,ServerId
        ,Year(DataUtilizacao)
        ,Month(DataUtilizacao)

    Declare @AnoReferencia INT = 0
            ,@MesReferencia INT = 0

    SELECT
        @AnoReferencia = [AnoReferencia]
       ,@MesReferencia = [MesReferencia]
    FROM [dbo].[ParamentroSistema]

	--Declare @MesAnoReferencia VARCHAR(7)
	
	--Select  @MesAnoReferencia = CAST(FORMAT(MesReferencia, '00') + '/' + FORMAT(AnoReferencia, '0000') AS VARCHAR(7))
	--Select  @MesAnoReferencia = CAST(FORMAT(AnoReferencia, '0000') + '/' + FORMAT(MesReferencia, '00') AS VARCHAR(7))
    --from    ParamentroSistema
	Delete from ConsumoMensal 
	Where	AnoConsumo = @AnoReferencia
	And		MesConsumo = @MesReferencia
	And		ProductId = @ProductIdCrivo

    -- Setar Flag = 0 (Registros ja importados) na Tabela de connsumoMensal Tempopraria 
    Update 
        #BO_MES
    Set
        Flag = 0
	FROM	ConsumoMensal AS A INNER JOIN #BO_MES AS B 
			ON	A.Cnpj = B.[NR_CNPJ] 
			AND A.TipoTarifacao = B.[NM_TP_TARIFACAO] 
            AND A.CC = B.CC
            --And A.ServerId = B.SERVERID / O ServerID na tabela consumo Mensal é sempre 99999
			AND A.AnoConsumo = B.[ANO_CONSULTA] 
			AND A.MesConsumo = B.[MES_CONSULTA]
            AND A.ProductId = @ProductIdCrivo

    Delete from #BO_MES Where Flag = 0 -- Apago os registro que ja foram importados
    
 --   if @Debug = 1
 --   Begin
 --       -- 25076 linhas
 --       --select * from #BO_MES --Where ANO_CONSULTA = 2020 And MES_CONSULTA = 6
 --       select COUNT(*) from ConsumoMensal
 --       Select COUNT(*) from #BO_MES
 --       Select B.*
	--    FROM	ConsumoMensal AS A INNER JOIN #BO_MES AS B 
	--		    ON	A.Cnpj = B.[NR_CNPJ] 
	--		    AND A.TipoTarifacao = B.[NM_TP_TARIFACAO] 
 --               and A.CC = B.CC
 --               --And A.ServerId = B.SERVERID / O ServerID na tabela consumo Mensal é sempre 99999
	--		    AND A.AnoConsumo = B.[ANO_CONSULTA] 
	--		    and A.MesConsumo = B.[MES_CONSULTA]
	--End

    -- STATUS 2 SE TORNA 1
	UPDATE	ConsumoMensal
	SET		StatusMensal = 1
	WHERE   StatusMensal = 2

	-- Update
	UPDATE	
        ConsumoMensal 
	SET		
        QuantidadeConsumo = B.QT_UTILIZA
	FROM	
        ConsumoMensal AS A INNER JOIN #BO_MES AS B ON A.Cnpj = B.[NR_CNPJ] 
		    AND A.TipoTarifacao = B.[NM_TP_TARIFACAO] 
		    AND A.AnoConsumo = B.[ANO_CONSULTA] 
		    and A.MesConsumo = B.[MES_CONSULTA]
			and A.CC = B.CC
	WHERE	A.StatusMensal = 1
	 
	--INSERÇÃO
	IF OBJECT_ID('tempdb..#ConsumoMensal', 'U') IS NOT NULL 
		DROP TABLE #ConsumoMensal
    
    --truncate table ConsumoMensal
	SELECT	B.[NR_CNPJ] Cnpj
			,B.[NM_TP_TARIFACAO] TipoTarifacao
            ,B.CC CC
            ,B.SERVERID ServerId
			,B.ANO_CONSULTA AnoConsumo
			,B.MES_CONSULTA MesConsumo
			,B.[QT_UTILIZA] QuantidadeConsumo
			,1 StatusMensal
            ,0 QtDiasConsumo
            ,0 ValorTotalConsumo
			,@ProductIdCrivo ProductId
    INTO    #ConsumoMensal
	FROM	ConsumoMensal AS A RIGHT JOIN #BO_MES AS B 
			ON	A.Cnpj = B.[NR_CNPJ] 
			AND A.TipoTarifacao = B.[NM_TP_TARIFACAO] 
			AND A.AnoConsumo = B.[ANO_CONSULTA] 
			and A.MesConsumo = B.[MES_CONSULTA]
			and A.CC = B.CC
            and A.ServerId = B.SERVERID
	WHERE	A.Cnpj IS NULL

    --if @Debug = 1
    --Begin
    --    Select * from #ConsumoMensal 
    --End

	-- Verifica se tem todas as linhas
	IF OBJECT_ID('tempdb..#Temp', 'U') IS NOT NULL 
		DROP TABLE #Temp

	CREATE TABLE #Temp
	(  
		CNPJ bigint, 
		TPTARIFACAO nvarchar(50),
		CC nvarchar(50),
        SERVERID bigint,
		Ano int, 
		Mes int,
		NrDias int
	);

	Insert into #Temp (
        CNPJ 
        ,TPTARIFACAO 
		,CC
        ,SERVERID
        ,Ano
        ,Mes 
        ,NrDias
    ) 
	Select	
        Cnpj 
        ,TipoTarifacao
		,CC
        ,ServerId
        ,Ano
        ,Mes
        ,count(Dia) NrDias
	FROM 
        (
            Select	Distinct Cnpj, 
            TipoTarifacao,
			CC,
            ServerId,
            year(DataUtilizacao) as Ano, 
            month(DataUtilizacao) as Mes, 
            Day(DataUtilizacao) as Dia 
            from	[dbo].ConsumoDiario
        ) as A
	Where	Ano >= 2010
	Group by 
         Cnpj 
        ,TipoTarifacao
		,CC
        ,ServerId
        ,Ano
        ,Mes
	--Having count(Dia) < (dbo.diasdomes(Ano, Mes))
    --Select * from #Temp 
    --Where Cnpj = 34020354000110
    --And Ano = 2020

	-- Colocar os dias de consumo por cliente
    UPDATE	
        #ConsumoMensal
	SET		
        #ConsumoMensal.QtDiasConsumo = Table_B.NrDias
	FROM	
        #ConsumoMensal AS Table_A, 
		#Temp AS Table_B
	WHERE   Table_A.Cnpj = Table_B.CNPJ
	AND		Table_A.AnoConsumo = Table_B.Ano
	AND		Table_A.MesConsumo = Table_B.Mes
	AND		Table_A.CC = Table_B.CC
    AND     Table_A.ServerId = Table_B.SERVERID
	AND		Table_A.TipoTarifacao = Table_B.TPTARIFACAO

    --Select * from #ConsumoMensal 
    --Where Cnpj = 34020354000110
    --And AnoConsumo = 2020

	-- Mudo status para os que não tem todos os dias
    UPDATE	
        #ConsumoMensal
	SET		
        #ConsumoMensal.StatusMensal = 2
		,QtDiasConsumo = Table_B.NrDias
	FROM	
        #ConsumoMensal AS Table_A, 
		#Temp AS Table_B
	WHERE   Table_A.Cnpj = Table_B.CNPJ
	AND		Table_A.AnoConsumo = Table_B.Ano
	AND		Table_A.MesConsumo = Table_B.Mes
	AND		Table_A.TipoTarifacao = Table_B.TPTARIFACAO
	AND		Table_A.CC = Table_B.CC
    AND     Table_A.ServerId = Table_B.SERVERID
    AND     Table_A.QtDiasConsumo < (dbo.diasdomes(Ano, Mes))

    -- Deixar o ultimo mes para Provisões
    UPDATE	
        #ConsumoMensal
	SET		
        #ConsumoMensal.StatusMensal = 1
	FROM	
        #ConsumoMensal 
	WHERE  AnoConsumo = @AnoReferencia And MesConsumo = @MesReferencia

    --If @Debug = 1
    --Begin
    --	Select  * From #ConsumoMensal 
    --    Where cnpj = 3634220000165
    --    order by
		  --  Cnpj
		  --  ,TipoTarifacao
    --        ,CC
		  --  ,AnoConsumo
		  --  ,MesConsumo
		  --  ,StatusMensal
    --    --Where   cnpj = 3634220000165
    --    --AND     TipoTarifacao = 'MMRepartida3'
    --    --AND     CC = 'CNH'
    --    --AND     ServerId = 99999
    --    --AND     AnoConsumo = 2020
    --    --AND     MesConsumo = 6
    --    --AND     ProductId = '311111120'

    --	--Select  *
    -- --   FROM    ConsumoMensal AS A INNER JOIN #ConsumoMensal AS B 
			 -- --  ON	A.Cnpj = B.Cnpj 
			 -- --  AND A.TipoTarifacao = B.TipoTarifacao
    -- --           and A.CC = B.CC
			 -- --  AND A.AnoConsumo = B.AnoConsumo
			 -- --  and A.MesConsumo = B.MesConsumo
    -- --   Where   B.StatusMensal = 1
    --End

    INSERT INTO ConsumoMensal 
	(
		Cnpj
		,TipoTarifacao
        ,CC
        ,ServerId
		,AnoConsumo
		,MesConsumo
		,QuantidadeConsumo
        ,ValorTotalConsumo
		,StatusMensal
  		,ProductId
	)
	SELECT	 
		Cnpj
		,TipoTarifacao
        ,CC
        ,99999
		,AnoConsumo
		,MesConsumo
		,sum(QuantidadeConsumo) QuantidadeConsumo
        ,0
        ,1
        ,'311111120'
	FROM    #ConsumoMensal
    Where   StatusMensal = 1
    Group By
		Cnpj
		,TipoTarifacao
        ,CC
		,AnoConsumo
		,MesConsumo
    order by
		Cnpj
		,TipoTarifacao
        ,CC
		,AnoConsumo
		,MesConsumo

  --  Update 
  --      ConsumoMensal
  --  Set
		--StatusMensal = 1
  --      ,QtDiasConsumo = B.QtDiasConsumo
  --      ,ValorTotalConsumo = B.ValorTotalConsumo
		--,ProductId = B.ProductId
  --  FROM    ConsumoMensal AS A INNER JOIN #ConsumoMensal B
		--	ON	A.Cnpj = B.Cnpj 
		--	AND A.TipoTarifacao = B.TipoTarifacao
  --          and A.CC = B.CC
		--	AND A.AnoConsumo = B.AnoConsumo
		--	and A.MesConsumo = B.MesConsumo
    --Select * from #ConsumoMensal
    --Select * from ConsumoMensal 


    --if @Debug = 1
    --Begin
	   -- SELECT	 
		  --  Cnpj
		  --  ,TipoTarifacao
    --        ,CC
    --        ,99999
		  --  ,AnoConsumo
		  --  ,MesConsumo
		  --  ,sum(QuantidadeConsumo) 
		  --  ,StatusMensal
    --        ,QtDiasConsumo
    --        ,[ValorTotalConsumo]
		  --  ,ProductId
    --        ,COUNT(Cnpj) As QtReg
	   -- FROM  ConsumoMensal
    --    --Where StatusMensal = 1
    --    Group By
		  --  Cnpj
		  --  ,TipoTarifacao
    --        ,CC
		  --  ,AnoConsumo
		  --  ,MesConsumo
		  --  ,StatusMensal
    --        ,QtDiasConsumo
    --        ,[ValorTotalConsumo]
		  --  ,ProductId
    --    order by
		  --  Cnpj
		  --  ,TipoTarifacao
    --        ,CC
		  --  ,AnoConsumo
		  --  ,MesConsumo
		  --  ,StatusMensal
    --End

    Delete from ResultadoCriticas
    Where Origem = 'AtualizarConsumoMensal' 
    
    Insert Into ResultadoCriticas
    (
        [Cnpj]
        ,[Ano]
        ,[Mes]
        ,[IdContrato]
        ,[Descricao]
        ,Origem
    )
    SELECT distinct
        [Cnpj]
        ,[AnoConsumo]
        ,[MesConsumo]
        ,0
        ,'Quantidade de dias invalido.'
        ,'AtualizarConsumoMensal'
    FROM 
        ConsumoMensal
    Where StatusMensal = 2
    And   AnoConsumo >= 2019
    order by
        Cnpj,
        AnoConsumo,
        MesConsumo
END



 --   INSERT INTO ConsumoMensal 
	--(
	--	Cnpj
	--	,TipoTarifacao
 --       ,CC
 --       ,ServerId
	--	,AnoConsumo
	--	,MesConsumo
	--	,QuantidadeConsumo
	--	,StatusMensal
 --       ,QtDiasConsumo
 --       ,[ValorTotalConsumo]
	--	,ProductId
	--)
	--SELECT	B.[NR_CNPJ]
	--		,B.[NM_TP_TARIFACAO]
 --           ,B.CC
 --           ,B.SERVERID
	--		,B.ANO_CONSULTA
	--		,B.MES_CONSULTA
	--		,B.[QT_UTILIZA]
	--		,1
 --           ,0
 --           ,0
	--		,'311111120'
	--FROM	ConsumoMensal AS A RIGHT JOIN #BO_MES AS B 
	--		ON	A.Cnpj = B.[NR_CNPJ] 
	--		AND A.TipoTarifacao = B.[NM_TP_TARIFACAO] 
	--		AND A.AnoConsumo = B.[ANO_CONSULTA] 
	--		and A.MesConsumo = B.[MES_CONSULTA]
	--		and A.CC = B.CC
	--WHERE	A.Cnpj IS NULL


    --Select  CAST(FORMAT(@AnoConsumo, '0000') + FORMAT(@MesConsumo, '00') AS INT)
    --       ,CAST(FORMAT(@AnoReferencia, '0000') + FORMAT(@MesReferencia, '00') AS INT)
    --IF CAST(FORMAT(@AnoConsumo, '0000') + FORMAT(@MesConsumo, '00') AS INT) > CAST(FORMAT(@AnoReferencia, '0000') + FORMAT(@MesReferencia, '00') AS INT)
    --BEGIN
    --    SET @Loop = 1
    --END
    --Select  @AnoReferencia, @MesReferencia
    --select * from #BO_MES 
    --Where ANO_CONSULTA = 2020
    --And MES_CONSULTA = 6

    --select * from #BO_MES where CAST(FORMAT(ANO_CONSULTA, '0000') + FORMAT(MES_CONSULTA, '00') AS INT) >= CAST(FORMAT(@AnoReferencia, '0000') + FORMAT(@MesReferencia, '00') AS INT)




*/