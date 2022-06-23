USE [DesafioDotNet]
IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'GetAllProducts'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetAllProducts

	 IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'GetById'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.GetById

