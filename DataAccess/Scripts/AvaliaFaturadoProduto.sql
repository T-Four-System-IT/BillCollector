USE MyCompanyTest
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

If Object_Id('dbo.AvaliaFaturadoProduto') is not null
begin
   drop procedure dbo.AvaliaFaturadoProduto
end
go

CREATE PROCEDURE AvaliaFaturadoProduto
AS
BEGIN
  
	/*	1 - Novo
	*	2_- Cliente não encontrado na tabela de cliente
	*	3 - Produto não encontrato
	*	4 - Cliente não possui este produto cadastrado
	*	50 - Pronto para Avaliação de Ação
	*	99 - Baixado
	*/

	Update	FaturadoProduto
	Set		StatusFaturados = 1

	-- 99 - Baixado
	Update	FaturadoProduto
	Set		StatusFaturados = 99
	from	FaturadoProduto
	Where	StatusFaturados = 1
	And		DtBaixa is not null

	-- 2 - Clientes não encontrados na tabela de cliente
	Update	FaturadoProduto
	Set		StatusFaturados = 2
	from	FaturadoProduto LEFT JOIN Clientes
			ON FaturadoProduto.CNPJCPF = Clientes.Cnpj  
	Where	Cnpj is null
	And		FaturadoProduto.StatusFaturados = 1

	-- 3 - Produto não cadastrado
	Update	FaturadoProduto
	Set		StatusFaturados = 3
	from	FaturadoProduto LEFT JOIN Produtos
			ON FaturadoProduto.Descricao = Produtos.NomeProduto
	Where	NomeProduto is null
	And		FaturadoProduto.StatusFaturados = 1

	-- 4 - Cliente não possui este produto cadastrado
	Update	FaturadoProduto
	Set		StatusFaturados = 3
	from	FaturadoProduto LEFT JOIN Produtos
			ON FaturadoProduto.Descricao = Produtos.NomeProduto
	Where	NomeProduto is null
	And		FaturadoProduto.StatusFaturados = 1

	-- 4 - Cliente não possui este produto cadastrado
	Update	FaturadoProduto
	Set		StatusFaturados = 4
	FROM	dbo.FaturadoProduto LEFT JOIN dbo.Clientes
			ON FaturadoProduto.CNPJCPF = Clientes.Cnpj
			LEFT JOIN dbo.ProdutosCliente
			ON ProdutosCliente.ClienteID = Clientes.ID
	Where	StatusFaturados = 1
	And		ProdutoID is NULL

	Update	FaturadoProduto
	Set		StatusFaturados = 50
	from	FaturadoProduto
	Where	StatusFaturados = 1 

	select	FaturadoProduto.StatusFaturados, * from	FaturadoProduto

END
