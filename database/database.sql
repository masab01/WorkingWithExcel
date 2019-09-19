USE [master]
GO

/****** Object:  Database [Bank]    Script Date: 19.09.2019 17:31:13 ******/
CREATE DATABASE [Bank]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bank', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Bank.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Bank_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Bank_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Bank] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bank].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Bank] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Bank] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Bank] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Bank] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Bank] SET ARITHABORT OFF 
GO

ALTER DATABASE [Bank] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Bank] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Bank] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Bank] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Bank] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Bank] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Bank] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Bank] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Bank] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Bank] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Bank] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Bank] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Bank] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Bank] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Bank] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Bank] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Bank] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Bank] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Bank] SET  MULTI_USER 
GO

ALTER DATABASE [Bank] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Bank] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Bank] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Bank] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Bank] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Bank] SET  READ_WRITE 
GO

