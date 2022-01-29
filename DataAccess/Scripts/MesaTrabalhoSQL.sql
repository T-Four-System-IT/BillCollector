SELECT
		ReguaCobranca.ID
		,ReguaCobranca.DescricaoRegua
		,ReguaCobranca.Vigencia
		,ReguaCobranca.StatusRegua
		,ReguaTemplate.NomeAcao
		,ReguaTemplate.TipoCliente
		,ReguaTemplate.QuantidadeDiasAtraso
		,ReguaTemplate.CriaArqBloqueio
		,ReguaTemplate.CopiaGestorComercial
		,ReguaTemplate.CopiaDiretorComercial
		,TemplateEmails.Descricao
FROM	dbo.ReguaTemplate INNER JOIN dbo.ReguaCobranca
		ON ReguaTemplate.ReguaCobrancaID = ReguaCobranca.ID
		INNER JOIN dbo.TemplateEmails
		ON ReguaTemplate.TemplateEmailsID = TemplateEmails.ID
Where	ReguaCobranca.StatusRegua = 1


Select	*
From	ReguaCobranca	
--Where	StatusRegua = 1

Select	*
From	ReguaTemplate


/*
Insert into ReguaCobranca
SELECT [DescricaoRegua]
      ,[Vigencia]
      ,[UsuarioID]
      ,[DataManutencao]
      ,[StatusRegua]
  FROM [dbo].[ReguaCobranca]


Insert into ReguaTemplate
SELECT [NomeAcao]
      ,[TipoCliente]
      ,[QuantidadeDiasAtraso]
      ,[CriaArqBloqueio]
      ,[CopiaGestorComercial]
      ,[CopiaDiretorComercial]
      ,[UsuarioID]
      ,[DataManutencao]
      ,[TemplateEmailsID]
      ,ReguaCobrancaID
  FROM [dbo].[ReguaTemplate]
Where ReguaCobrancaID = 2




*/
