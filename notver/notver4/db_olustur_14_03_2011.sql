USE [master]
GO
/****** Object:  Database [notver2]    Script Date: 03/14/2011 20:09:55 ******/
CREATE DATABASE [notver2] ON  PRIMARY 
( NAME = N'notver2', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\notver2.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'notver2_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\notver2_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE Turkish_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'notver2', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [notver2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [notver2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [notver2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [notver2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [notver2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [notver2] SET ARITHABORT OFF 
GO
ALTER DATABASE [notver2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [notver2] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [notver2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [notver2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [notver2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [notver2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [notver2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [notver2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [notver2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [notver2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [notver2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [notver2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [notver2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [notver2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [notver2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [notver2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [notver2] SET  READ_WRITE 
GO
ALTER DATABASE [notver2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [notver2] SET  MULTI_USER 
GO
ALTER DATABASE [notver2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [notver2] SET DB_CHAINING OFF 