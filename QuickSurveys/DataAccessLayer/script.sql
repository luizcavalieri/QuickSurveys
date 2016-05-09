USE [master]
GO
/****** Object:  Database [DataBaseTestMigration]    Script Date: 5/9/2016 4:16:58 PM ******/
CREATE DATABASE [DataBaseTestMigration]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataBaseTestMigration', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DataBaseTestMigration.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DataBaseTestMigration_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DataBaseTestMigration_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DataBaseTestMigration] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataBaseTestMigration].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataBaseTestMigration] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DataBaseTestMigration] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DataBaseTestMigration] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataBaseTestMigration] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataBaseTestMigration] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DataBaseTestMigration] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataBaseTestMigration] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DataBaseTestMigration] SET  MULTI_USER 
GO
ALTER DATABASE [DataBaseTestMigration] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataBaseTestMigration] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataBaseTestMigration] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataBaseTestMigration] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DataBaseTestMigration]
GO
/****** Object:  Table [dbo].[answer_group]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[answer_group](
	[answer_group_id] [int] IDENTITY(1,1) NOT NULL,
	[answer_group_max_option] [int] NULL,
	[answer_group_description] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[answer_group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[answer_group_option]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[answer_group_option](
	[answer_group_option_id] [int] NOT NULL,
	[answer_group_option_desc] [varchar](255) NOT NULL,
	[answer_group_id] [int] NOT NULL,
	[answer_group_logical_answer] [bit] NULL,
 CONSTRAINT [PK__answer_g__871FC6C4F522A54D] PRIMARY KEY CLUSTERED 
(
	[answer_group_option_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[answers]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[answers](
	[answer_id] [int] IDENTITY(1,1) NOT NULL,
	[answer_numeric] [int] NULL,
	[answer_text] [varchar](255) NULL,
	[answer_boolean] [bit] NULL,
	[answer_resp_id] [int] NOT NULL,
	[answer_question_id] [int] NULL,
	[answer_group_option_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[input_type]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[input_type](
	[input_type_id] [int] IDENTITY(1,1) NOT NULL,
	[input_type_desc] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[input_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[questions]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[questions](
	[quest_id] [int] IDENTITY(1,1) NOT NULL,
	[quest_description] [varchar](255) NOT NULL,
	[quest_answer_group_id] [int] NULL,
	[quest_required] [bit] NULL,
	[quest_input_type_id] [int] NOT NULL,
	[quest_multi_answer] [bit] NULL,
	[quest_survey_id] [int] NOT NULL,
	[quest_survey_sequence] [int] NULL,
	[quest_main_id] [int] NULL,
	[quest_child_sequence] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[quest_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[respondents]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[respondents](
	[resp_id] [int] IDENTITY(1,1) NOT NULL,
	[resp_gender] [varchar](20) NULL,
	[resp_age_range] [varchar](25) NULL,
	[resp_state_territory] [varchar](5) NULL,
	[resp_email] [varchar](75) NULL,
	[resp_home_suburb] [varchar](75) NULL,
	[res_home_post_code] [int] NULL,
	[resp_work_post_code] [int] NULL,
	[resp_work_suburb] [varchar](75) NULL,
	[resp_dob] [datetime] NULL,
	[resp_date] [timestamp] NOT NULL,
	[resp_IP] [varchar](50) NULL,
	[resp_user_id] [int] NULL,
	[resp_survey_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[resp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[surveys]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[surveys](
	[survey_id] [int] IDENTITY(1,1) NOT NULL,
	[survey_description] [varchar](255) NOT NULL,
	[survey_user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[survey_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[user_fname] [varchar](255) NOT NULL,
	[user_lname] [varchar](255) NOT NULL,
	[user_password] [varchar](255) NOT NULL,
	[user_role] [varchar](255) NOT NULL,
	[user_pref_phone] [varchar](255) NULL,
	[user_dob] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[QuestionsBySurvey]    Script Date: 5/9/2016 4:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[QuestionsBySurvey]
AS
SELECT        dbo.questions.quest_id, dbo.questions.quest_description, dbo.questions.quest_answer_group_id, dbo.questions.quest_required, dbo.questions.quest_input_type_id, dbo.questions.quest_multi_answer, 
                         dbo.questions.quest_survey_id, dbo.questions.quest_survey_sequence, dbo.questions.quest_main_id, dbo.questions.quest_child_sequence, dbo.input_type.input_type_desc, dbo.surveys.survey_description, 
                         dbo.surveys.survey_user_id, dbo.surveys.survey_id, dbo.input_type.input_type_id
FROM            dbo.questions INNER JOIN
                         dbo.input_type ON dbo.questions.quest_input_type_id = dbo.input_type.input_type_id INNER JOIN
                         dbo.surveys ON dbo.questions.quest_survey_id = dbo.surveys.survey_id
WHERE        (dbo.surveys.survey_id = 1)

GO
SET IDENTITY_INSERT [dbo].[answer_group] ON 

INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (1, 4, N'Bank')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (2, 1, N'ISP')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (3, 2, N'car make')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (4, NULL, N'age range')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (5, NULL, N'banks service')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (6, NULL, N'state')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (7, NULL, N'iSP service')
INSERT [dbo].[answer_group] ([answer_group_id], [answer_group_max_option], [answer_group_description]) VALUES (8, NULL, N'Gender')
SET IDENTITY_INSERT [dbo].[answer_group] OFF
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (1, N'CBA', 1, 1)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (2, N'NAB', 1, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (3, N'ANZ', 1, 1)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (4, N'Westpac', 1, 1)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (5, N'Citibank', 1, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (6, N'St George', 1, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (7, N'TPG', 2, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (8, N'Telstra', 2, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (9, N'Vodafone', 2, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (10, N'optus', 2, 1)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (11, N'dodo', 2, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (12, N'iinet', 2, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (13, N'ford', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (14, N'wolkswagen', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (15, N'holden', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (16, N'audi', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (17, N'tesla', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (18, N'land rover', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (19, N'bmw', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (20, N'honda', 3, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (21, N'18 to 23', 4, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (22, N'24 to 29', 4, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (23, N'30 to 35', 4, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (24, N'35 to 40', 4, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (25, N'40 +', 4, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (26, N'internet bank', 5, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (27, N'home loan', 5, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (28, N'credit card', 5, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (29, N'share investment', 5, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (30, N'wa', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (31, N'sa', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (32, N'vic', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (33, N'nsw', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (34, N'qld', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (35, N'nt', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (36, N'act', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (37, N'taz', 6, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (38, N'fetch tv', 7, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (39, N'netflix', 7, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (40, N'foxtel', 7, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (41, N'male', 8, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (42, N'female', 8, 0)
INSERT [dbo].[answer_group_option] ([answer_group_option_id], [answer_group_option_desc], [answer_group_id], [answer_group_logical_answer]) VALUES (43, N'other', 8, 0)
SET IDENTITY_INSERT [dbo].[answers] ON 

INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (3, NULL, N' ', 0, 1, 7, 3)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (4, NULL, N' ', 0, 1, 8, 26)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (5, NULL, N' ', 0, 1, 9, 19)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (6, NULL, N' ', 0, 1, 10, 12)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (7, 3245, N' ', 0, 1, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (8, NULL, N' ', 0, 1, 13, 23)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (9, NULL, N' ', 0, 1, 14, 43)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (10, NULL, N' ', 0, 1, 15, 23)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (11, NULL, N' ', 0, 2, 7, 5)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (12, NULL, N' ', 0, 2, 9, 15)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (13, NULL, N' ', 0, 2, 10, 9)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (14, 1245, N' ', 0, 2, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (15, NULL, N' ', 0, 2, 13, 25)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (16, NULL, N' ', 0, 2, 14, 41)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (17, NULL, N' ', 0, 2, 15, 25)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (18, NULL, N' ', 0, 3, 7, 3)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (19, NULL, N' ', 0, 3, 8, 27)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (20, NULL, N' ', 0, 3, 9, 14)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (21, NULL, N' ', 0, 3, 10, 10)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (22, NULL, N' ', 0, 3, 11, 38)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (23, 2098, N' ', 0, 3, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (24, NULL, N' ', 0, 3, 13, 24)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (25, NULL, N' ', 0, 3, 14, 42)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (26, NULL, N' ', 0, 3, 15, 24)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (27, NULL, N' ', 0, 4, 7, 2)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (28, NULL, N' ', 0, 4, 9, 19)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (29, NULL, N' ', 0, 4, 10, 11)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (30, 2304, N' ', 0, 4, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (31, NULL, N' ', 0, 4, 13, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (32, NULL, N' ', 0, 4, 14, 43)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (33, NULL, N' ', 0, 4, 15, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (34, NULL, N' ', 0, 5, 7, 1)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (35, NULL, N' ', 0, 5, 8, 28)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (36, NULL, N' ', 0, 5, 9, 20)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (37, NULL, N' ', 0, 5, 10, 11)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (38, 2000, N' ', 0, 5, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (39, NULL, N' ', 0, 5, 13, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (40, NULL, N' ', 0, 5, 14, 43)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (41, NULL, N' ', 0, 5, 15, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (42, NULL, N' ', 0, 6, 7, 2)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (43, NULL, N' ', 0, 6, 9, 15)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (44, NULL, N' ', 0, 6, 10, 11)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (45, 3459, N' ', 0, 6, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (46, NULL, N' ', 0, 6, 13, 24)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (47, NULL, N' ', 0, 6, 14, 43)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (48, NULL, N' ', 0, 6, 15, 24)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (49, NULL, N' ', 0, 7, 7, 5)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (50, NULL, N' ', 0, 7, 9, 20)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (51, NULL, N' ', 0, 7, 10, 12)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (52, 4567, N' ', 0, 7, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (53, NULL, N' ', 0, 7, 13, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (54, NULL, N' ', 0, 7, 14, 41)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (55, NULL, N' ', 0, 7, 15, 21)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (56, NULL, N' ', 0, 8, 7, 1)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (57, NULL, N' ', 0, 8, 8, 29)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (58, NULL, N' ', 0, 8, 9, 19)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (59, NULL, N' ', 0, 8, 10, 9)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (60, 2567, N' ', 0, 8, 12, NULL)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (61, NULL, N' ', 0, 8, 13, 25)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (62, NULL, N' ', 0, 8, 14, 43)
INSERT [dbo].[answers] ([answer_id], [answer_numeric], [answer_text], [answer_boolean], [answer_resp_id], [answer_question_id], [answer_group_option_id]) VALUES (63, NULL, N' ', 0, 8, 15, 25)
SET IDENTITY_INSERT [dbo].[answers] OFF
SET IDENTITY_INSERT [dbo].[input_type] ON 

INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (1, N'checkbox')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (2, N'radio')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (3, N'text')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (4, N'date')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (5, N'color')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (6, N'datetime')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (7, N'datetime-local')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (8, N'email')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (9, N'month')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (10, N'number')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (11, N'range')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (12, N'search')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (13, N'tel')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (14, N'time')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (15, N'url')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (16, N'week')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (17, N'password')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (18, N'dropdown')
INSERT [dbo].[input_type] ([input_type_id], [input_type_desc]) VALUES (19, N'TextArea')
SET IDENTITY_INSERT [dbo].[input_type] OFF
SET IDENTITY_INSERT [dbo].[questions] ON 

INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (7, N'Bank Provider', 1, 0, 1, 1, 1, 1, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (8, N'Bank Service', 5, 0, 1, 1, 1, NULL, 1, 1)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (9, N'What is the brand of your car', 3, 0, 1, 1, 1, 2, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (10, N'What is your Intenet Service Provider', 2, 0, 2, 0, 1, 3, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (11, N'Are you currently using any one of the following services?', 7, 0, 1, 1, 1, NULL, 3, 1)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (12, N'What is your home post code', NULL, 1, 10, 0, 1, 4, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (13, N'What is your age range', 4, 1, 1, 0, 1, 5, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (14, N'What is your gender', 8, 1, 18, 0, 1, 6, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (15, N'What is your age range', 4, 1, 1, 0, 1, 7, NULL, NULL)
INSERT [dbo].[questions] ([quest_id], [quest_description], [quest_answer_group_id], [quest_required], [quest_input_type_id], [quest_multi_answer], [quest_survey_id], [quest_survey_sequence], [quest_main_id], [quest_child_sequence]) VALUES (16, N'Any comments on the research?', NULL, 1, 19, 0, 1, 8, NULL, NULL)
SET IDENTITY_INSERT [dbo].[questions] OFF
SET IDENTITY_INSERT [dbo].[respondents] ON 

INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (1, N'male', N'21', N'33', N'1test@gmail.com', N'sydney', 2304, 2314, N'sydney', CAST(0x0000000000000000 AS DateTime), N'10.234.256.34', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (2, N'male', N'25', N'34', N'2test@gmail.com', N'gold coast', 2567, 2573, N'gold coast', CAST(0x0000000000000000 AS DateTime), N'123.235.245.02', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (3, N'female', N'22', N'32', N'3test@gmail.com', N'melbourne', 2000, 2001, N'melbourne', CAST(0x0000000000000000 AS DateTime), N'156.234.987.23', 2, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (4, N'male', N'24', N'30', N'4test@gmail.com', N'perth', 3459, 3466, N'perth', CAST(0x0000000000000000 AS DateTime), N'156.234.987.23', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (5, N'female', N'21', N'30', N'5test@gmail.com', N'perth', 4567, 4570, N'perth', CAST(0x0000000000000000 AS DateTime), N'145.987.678.1', 3, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (6, N'female', N'21', N'30', N'6test@gmail.com', N'perth', 2000, 2003, N'perth', CAST(0x0000000000000000 AS DateTime), N'453.678.953.9', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (7, N'male', N'22', N'33', N'7test@gmail.com', N'sydney', 3245, 3252, N'sydney', CAST(0x0000000000000000 AS DateTime), N'123.235.245.02', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (8, N'female', N'23', N'34', N'8test@gmail.com', N'brisbane', 1245, 1246, N'brisbane', CAST(0x0000000000000000 AS DateTime), N'565.889.766.09', NULL, 1)
INSERT [dbo].[respondents] ([resp_id], [resp_gender], [resp_age_range], [resp_state_territory], [resp_email], [resp_home_suburb], [res_home_post_code], [resp_work_post_code], [resp_work_suburb], [resp_dob], [resp_IP], [resp_user_id], [resp_survey_id]) VALUES (9, N'other', N'23', N'31', N'9test@gmail.com', N'adelaide', 2098, 2102, N'adelaide', CAST(0x0000000000000000 AS DateTime), N'565.889.766.10', NULL, 1)
SET IDENTITY_INSERT [dbo].[respondents] OFF
SET IDENTITY_INSERT [dbo].[surveys] ON 

INSERT [dbo].[surveys] ([survey_id], [survey_description], [survey_user_id]) VALUES (1, N'Market research Behaviour', 1)
INSERT [dbo].[surveys] ([survey_id], [survey_description], [survey_user_id]) VALUES (2, N'MR Coca Cola', 1)
INSERT [dbo].[surveys] ([survey_id], [survey_description], [survey_user_id]) VALUES (3, N'MR Hilton Hotel', 4)
INSERT [dbo].[surveys] ([survey_id], [survey_description], [survey_user_id]) VALUES (4, N'MR AIT', 4)
SET IDENTITY_INSERT [dbo].[surveys] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [user_fname], [user_lname], [user_password], [user_role], [user_pref_phone], [user_dob]) VALUES (1, N'usertest1', N'user1', N'test1', N'abc123456', N'staff', N'46545446', CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[users] ([user_id], [username], [user_fname], [user_lname], [user_password], [user_role], [user_pref_phone], [user_dob]) VALUES (2, N'usertest2', N'user2', N'test2', N'abc123456', N'respondent', N'4544464', CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[users] ([user_id], [username], [user_fname], [user_lname], [user_password], [user_role], [user_pref_phone], [user_dob]) VALUES (3, N'usertest3', N'user3', N'test3', N'abc123456', N'respondent', N'54545444', CAST(0x0000000000000000 AS DateTime))
INSERT [dbo].[users] ([user_id], [username], [user_fname], [user_lname], [user_password], [user_role], [user_pref_phone], [user_dob]) VALUES (4, N'usertest4', N'user4', N'test4', N'abc123456', N'staff', N'5667687564', CAST(0x0000000000000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[users] OFF
ALTER TABLE [dbo].[answer_group_option]  WITH CHECK ADD  CONSTRAINT [FK__answer_gr__answe__1DE57479] FOREIGN KEY([answer_group_id])
REFERENCES [dbo].[answer_group] ([answer_group_id])
GO
ALTER TABLE [dbo].[answer_group_option] CHECK CONSTRAINT [FK__answer_gr__answe__1DE57479]
GO
ALTER TABLE [dbo].[answers]  WITH CHECK ADD FOREIGN KEY([answer_resp_id])
REFERENCES [dbo].[respondents] ([resp_id])
GO
ALTER TABLE [dbo].[answers]  WITH CHECK ADD FOREIGN KEY([answer_question_id])
REFERENCES [dbo].[questions] ([quest_id])
GO
ALTER TABLE [dbo].[answers]  WITH CHECK ADD  CONSTRAINT [FK__answers__answer___20C1E124] FOREIGN KEY([answer_group_option_id])
REFERENCES [dbo].[answer_group_option] ([answer_group_option_id])
GO
ALTER TABLE [dbo].[answers] CHECK CONSTRAINT [FK__answers__answer___20C1E124]
GO
ALTER TABLE [dbo].[questions]  WITH CHECK ADD  CONSTRAINT [FK__questions__quest__21B6055D] FOREIGN KEY([quest_answer_group_id])
REFERENCES [dbo].[answer_group] ([answer_group_id])
GO
ALTER TABLE [dbo].[questions] CHECK CONSTRAINT [FK__questions__quest__21B6055D]
GO
ALTER TABLE [dbo].[questions]  WITH CHECK ADD FOREIGN KEY([quest_input_type_id])
REFERENCES [dbo].[input_type] ([input_type_id])
GO
ALTER TABLE [dbo].[questions]  WITH CHECK ADD FOREIGN KEY([quest_survey_id])
REFERENCES [dbo].[surveys] ([survey_id])
GO
ALTER TABLE [dbo].[questions]  WITH CHECK ADD  CONSTRAINT [FK_questions_questions] FOREIGN KEY([quest_id])
REFERENCES [dbo].[questions] ([quest_id])
GO
ALTER TABLE [dbo].[questions] CHECK CONSTRAINT [FK_questions_questions]
GO
ALTER TABLE [dbo].[respondents]  WITH CHECK ADD FOREIGN KEY([resp_user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[respondents]  WITH CHECK ADD FOREIGN KEY([resp_survey_id])
REFERENCES [dbo].[surveys] ([survey_id])
GO
ALTER TABLE [dbo].[surveys]  WITH CHECK ADD FOREIGN KEY([survey_user_id])
REFERENCES [dbo].[users] ([user_id])
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[29] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "questions"
            Begin Extent = 
               Top = 92
               Left = 28
               Bottom = 222
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "input_type"
            Begin Extent = 
               Top = 0
               Left = 310
               Bottom = 153
               Right = 499
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "surveys"
            Begin Extent = 
               Top = 156
               Left = 309
               Bottom = 305
               Right = 554
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2670
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1230
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'QuestionsBySurvey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'QuestionsBySurvey'
GO
USE [master]
GO
ALTER DATABASE [DataBaseTestMigration] SET  READ_WRITE 
GO
