USE MyCompanyTest
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

If Object_Id('dbo.GeraAcaoReguaCobranca') is not null
begin
   drop procedure dbo.GeraAcaoReguaCobranca
end
go

CREATE PROCEDURE GeraAcaoReguaCobranca
AS
BEGIN
	IF OBJECT_ID('tempdb..#Temp', 'U') IS NOT NULL DROP TABLE #Temp

	Create table #Temp
	(
		ID				int IDENTITY(1,1) NOT NULL
		,FaturadoProdutoID int not null
		,NomeProduto	varchar(100) NOT NULL
		,CNPJCPF		Bigint NOT NULL
		,TipoCliente	varchar(1)
		,Vencimento		date NOT NULL
		,DiasAtraso [int] not null
	)

	INSERT INTO #Temp
	(
		FaturadoProdutoID
		,NomeProduto
		,[CNPJCPF]
		,TipoCliente
		,[Vencimento]
		,DiasAtraso
	)
	Select	FaturadoProduto.ID
			,Descricao
			,CNPJCPF
			,Clientes.TipoCliente
			,Vencimento
			,DATEDIFF ( day , Vencimento , Getdate() ) + 30 Dias
	From	FaturadoProduto Inner Join Clientes
			ON FaturadoProduto.CNPJCPF = clientes.Cnpj
	Where	StatusFaturados = 50

	Declare @Contador int = 1,
			@NumRegs int = (select count(*) from #Temp),
			@DiasAtrasoCalc int,
			@DiasAtraso int = -5,
			@NomeProduto	varchar(100),
			@CNPJCPF		Bigint,
			@Vencimento		date,
			@TipoCliente	varchar(1),
			@UsuarioID int = 4,
			@FaturadoProdutoID int

	While @Contador <= @NumRegs
	BEGIN
		Select 
				@NomeProduto = NomeProduto
				,@CNPJCPF = CNPJCPF
				,@TipoCliente = TipoCliente
				,@Vencimento = Vencimento
				,@DiasAtrasoCalc = DiasAtraso 
				,@FaturadoProdutoID = FaturadoProdutoID
		from	#Temp
		Where	ID = @Contador

		--Select @DiasAtrasoCalc, @TipoCliente

		-- Qual ou quais as ações foram feitas para este cliente
		--SELECT	'CobrancaAcao' Tabela
		--		,[ID]
		--		,[NomeProduto]
		--		,[CNPJCPF]
		--		,[Vencimento]
		--		,[FaturadosID]
		--		,[ReguaTemplateID]
		--		,[TemplateEmailsID]
		--		,[UsuarioID]
		--		,[DataManutencao]
		--FROM	[CobrancaAcao]
		--Where	NomeProduto = @NomeProduto
		--And		CNPJCPF = @CNPJCPF
		--And		Vencimento = @Vencimento
		--And		StatusAcao = 1

		INSERT INTO [dbo].[CobrancaAcao]
		(
			   [NomeProduto]
			   ,[CNPJCPF]
			   ,[Vencimento]
			   ,[FaturadosID]
			   ,[ReguaTemplateID]
			   ,[TemplateEmailsID]
			   ,[UsuarioID]
			   ,[DataManutencao]
			   ,[StatusAcao]
		)
		SELECT 
			   @NomeProduto NomeProduto
			   ,@CNPJCPF CNPKCPF
			   ,@Vencimento Vencimento
			   ,@FaturadoProdutoID FaturadoProdutoId
			   ,ReguaTemplate.ID ReguaTemplateID
			   ,ReguaTemplate.TemplateEmailsID TemplateEmailsID
			   ,@UsuarioID UsuarioID
			   ,GETDATE() DataManutencao
			   ,1 StatusAcao
		from	ReguaCobranca inner join ReguaTemplate
				ON ReguaCobranca.ID = ReguaTemplate.ReguaCobrancaID
		Where	QuantidadeDiasAtraso <= @DiasAtrasoCalc
		AND		ReguaTemplate.TipoCliente = @TipoCliente
		AND		ReguaTemplate.ID not in (	Select ReguaTemplateID
											FROM	[CobrancaAcao]
											Where	NomeProduto = @NomeProduto
											And		CNPJCPF = @CNPJCPF
											And		Vencimento = @Vencimento)
		Set @Contador += 1
	END
END
GO