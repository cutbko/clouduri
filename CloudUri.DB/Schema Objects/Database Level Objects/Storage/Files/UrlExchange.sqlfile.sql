﻿ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [UrlExchange], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\UrlExchange.mdf', SIZE = 3072 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

