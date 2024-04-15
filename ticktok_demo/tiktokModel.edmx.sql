
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/25/2024 08:38:39
-- Generated from EDMX file: C:\Users\user\source\repos\ticktok_demo\ticktok_demo\tiktokModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Tick];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_calender_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[calender] DROP CONSTRAINT [FK_calender_company];
GO
IF OBJECT_ID(N'[dbo].[FK_calender_country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[calender] DROP CONSTRAINT [FK_calender_country];
GO
IF OBJECT_ID(N'[dbo].[FK_calender_holiday]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[calender] DROP CONSTRAINT [FK_calender_holiday];
GO
IF OBJECT_ID(N'[dbo].[FK_company_client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[company] DROP CONSTRAINT [FK_company_client];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_employee_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee] DROP CONSTRAINT [FK_employee_company];
GO
IF OBJECT_ID(N'[dbo].[FK_employee_contact_country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee_contact] DROP CONSTRAINT [FK_employee_contact_country];
GO
IF OBJECT_ID(N'[dbo].[FK_employee_contact_employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee_contact] DROP CONSTRAINT [FK_employee_contact_employee];
GO
IF OBJECT_ID(N'[dbo].[FK_employee_holiday]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee] DROP CONSTRAINT [FK_employee_holiday];
GO
IF OBJECT_ID(N'[dbo].[FK_employee_job_desc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[employee] DROP CONSTRAINT [FK_employee_job_desc];
GO
IF OBJECT_ID(N'[dbo].[FK_holiday_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[holiday] DROP CONSTRAINT [FK_holiday_company];
GO
IF OBJECT_ID(N'[dbo].[FK_holiday_country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[holiday] DROP CONSTRAINT [FK_holiday_country];
GO
IF OBJECT_ID(N'[dbo].[FK_job_description_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[job_description] DROP CONSTRAINT [FK_job_description_company];
GO
IF OBJECT_ID(N'[dbo].[FK_leave_employee_approved_by]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[leave_employee] DROP CONSTRAINT [FK_leave_employee_approved_by];
GO
IF OBJECT_ID(N'[dbo].[FK_leave_employee_employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[leave_employee] DROP CONSTRAINT [FK_leave_employee_employee];
GO
IF OBJECT_ID(N'[dbo].[FK_leave_group_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[leave_group] DROP CONSTRAINT [FK_leave_group_company];
GO
IF OBJECT_ID(N'[dbo].[FK_project_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[project] DROP CONSTRAINT [FK_project_company];
GO
IF OBJECT_ID(N'[dbo].[FK_task_project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[task] DROP CONSTRAINT [FK_task_project];
GO
IF OBJECT_ID(N'[dbo].[FK_task_tracking_sheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[task] DROP CONSTRAINT [FK_task_tracking_sheet];
GO
IF OBJECT_ID(N'[dbo].[FK_tracking_sheet_company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tracking_sheet] DROP CONSTRAINT [FK_tracking_sheet_company];
GO
IF OBJECT_ID(N'[dbo].[FK_tracking_sheet_employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tracking_sheet] DROP CONSTRAINT [FK_tracking_sheet_employee];
GO
IF OBJECT_ID(N'[dbo].[FK_tracking_sheet_project]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tracking_sheet] DROP CONSTRAINT [FK_tracking_sheet_project];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[calender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[calender];
GO
IF OBJECT_ID(N'[dbo].[client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[client];
GO
IF OBJECT_ID(N'[dbo].[company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[company];
GO
IF OBJECT_ID(N'[dbo].[country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[country];
GO
IF OBJECT_ID(N'[dbo].[employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[employee];
GO
IF OBJECT_ID(N'[dbo].[employee_contact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[employee_contact];
GO
IF OBJECT_ID(N'[dbo].[holiday]', 'U') IS NOT NULL
    DROP TABLE [dbo].[holiday];
GO
IF OBJECT_ID(N'[dbo].[job_description]', 'U') IS NOT NULL
    DROP TABLE [dbo].[job_description];
GO
IF OBJECT_ID(N'[dbo].[leave_employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[leave_employee];
GO
IF OBJECT_ID(N'[dbo].[leave_group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[leave_group];
GO
IF OBJECT_ID(N'[dbo].[project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[project];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[task]', 'U') IS NOT NULL
    DROP TABLE [dbo].[task];
GO
IF OBJECT_ID(N'[dbo].[tracking_sheet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tracking_sheet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'calenders'
CREATE TABLE [dbo].[calenders] (
    [calender_id] uniqueidentifier  NOT NULL,
    [calender_year] int  NULL,
    [calender_month] int  NULL,
    [calender_week_01_start_date] datetime  NULL,
    [calender_week_01_end_date] datetime  NULL,
    [calender_week_02_start_date] datetime  NULL,
    [calender_week_02_end_date] datetime  NULL,
    [calender_week_03_start_date] datetime  NULL,
    [calender_week_03_end_date] datetime  NULL,
    [calender_week_04_start_date] datetime  NULL,
    [calender_week_04_end_date] datetime  NULL,
    [company_id] uniqueidentifier  NULL,
    [holiday_id] uniqueidentifier  NULL,
    [country_id] uniqueidentifier  NULL,
    [num_working_days] int  NULL
);
GO

-- Creating table 'clients'
CREATE TABLE [dbo].[clients] (
    [client_id] uniqueidentifier  NOT NULL,
    [client_name] varchar(max)  NULL
);
GO

-- Creating table 'companies'
CREATE TABLE [dbo].[companies] (
    [company_id] uniqueidentifier  NOT NULL,
    [company_name] varchar(max)  NULL,
    [client_id] uniqueidentifier  NULL
);
GO

-- Creating table 'countries'
CREATE TABLE [dbo].[countries] (
    [country_id] uniqueidentifier  NOT NULL,
    [country_name] varchar(max)  NULL
);
GO

-- Creating table 'employees'
CREATE TABLE [dbo].[employees] (
    [emp_id] uniqueidentifier  NOT NULL,
    [emp_first_name] varchar(max)  NULL,
    [emp_last_name] varchar(max)  NULL,
    [comp_id] uniqueidentifier  NULL,
    [holiday_id] uniqueidentifier  NULL,
    [user_id] nvarchar(128)  NULL,
    [job_des_id] uniqueidentifier  NULL,
    [pic] varchar(max)  NULL
);
GO

-- Creating table 'employee_contact'
CREATE TABLE [dbo].[employee_contact] (
    [emp_cont_id] uniqueidentifier  NOT NULL,
    [emp_address_line_01] varchar(max)  NULL,
    [emp_address_line_02] varchar(max)  NULL,
    [emp_address_city] varchar(max)  NULL,
    [emp_address_postalcode] varchar(50)  NULL,
    [emp_address_st_or_pr] varchar(max)  NULL,
    [emp_phone_01] varchar(50)  NULL,
    [emp_phone_02] varchar(50)  NULL,
    [country_id] uniqueidentifier  NULL,
    [employee_id] uniqueidentifier  NULL
);
GO

-- Creating table 'holidays'
CREATE TABLE [dbo].[holidays] (
    [holiday_id] uniqueidentifier  NOT NULL,
    [holiday_name] varchar(max)  NULL,
    [company_id] uniqueidentifier  NULL,
    [country_id] uniqueidentifier  NULL,
    [holiday_date] datetime  NULL
);
GO

-- Creating table 'job_description'
CREATE TABLE [dbo].[job_description] (
    [job_desc_id] uniqueidentifier  NOT NULL,
    [job_desc_name] varchar(max)  NULL,
    [company_id] uniqueidentifier  NULL
);
GO

-- Creating table 'leave_employee'
CREATE TABLE [dbo].[leave_employee] (
    [leave_emp_id] uniqueidentifier  NOT NULL,
    [leave_group_id] uniqueidentifier  NULL,
    [leave_start_date] datetime  NULL,
    [leave_end_date] datetime  NULL,
    [leave_reason] varchar(max)  NULL,
    [leave_status] varchar(50)  NULL,
    [approve_by_emp_id] uniqueidentifier  NULL,
    [employee_id] uniqueidentifier  NULL
);
GO

-- Creating table 'leave_group'
CREATE TABLE [dbo].[leave_group] (
    [leave_id] uniqueidentifier  NOT NULL,
    [leave_type] varchar(max)  NULL,
    [number_of_days] int  NULL,
    [company_id] uniqueidentifier  NULL
);
GO

-- Creating table 'projects'
CREATE TABLE [dbo].[projects] (
    [project_id] uniqueidentifier  NOT NULL,
    [project_name] varchar(max)  NULL,
    [project_des] varchar(max)  NULL,
    [company_id] uniqueidentifier  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'tasks'
CREATE TABLE [dbo].[tasks] (
    [task_id] uniqueidentifier  NOT NULL,
    [task_name] varchar(max)  NULL,
    [task_start_time] time  NULL,
    [task_end_time] time  NULL,
    [task_description] varchar(max)  NULL,
    [tracking_sheet_id] uniqueidentifier  NULL,
    [project_id] uniqueidentifier  NULL,
    [task_date] varchar(max)  NULL,
    [Hours] int  NULL,
    [Hours1] nvarchar(max)  NULL
);
GO

-- Creating table 'tracking_sheet'
CREATE TABLE [dbo].[tracking_sheet] (
    [tracking_id] uniqueidentifier  NOT NULL,
    [employee_id] uniqueidentifier  NULL,
    [tracking_date] datetime  NULL,
    [tracking_start_time] time  NULL,
    [tracking_end_time] time  NULL,
    [total_hours] int  NULL,
    [company_id] uniqueidentifier  NULL,
    [project_id] uniqueidentifier  NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [calender_id] in table 'calenders'
ALTER TABLE [dbo].[calenders]
ADD CONSTRAINT [PK_calenders]
    PRIMARY KEY CLUSTERED ([calender_id] ASC);
GO

-- Creating primary key on [client_id] in table 'clients'
ALTER TABLE [dbo].[clients]
ADD CONSTRAINT [PK_clients]
    PRIMARY KEY CLUSTERED ([client_id] ASC);
GO

-- Creating primary key on [company_id] in table 'companies'
ALTER TABLE [dbo].[companies]
ADD CONSTRAINT [PK_companies]
    PRIMARY KEY CLUSTERED ([company_id] ASC);
GO

-- Creating primary key on [country_id] in table 'countries'
ALTER TABLE [dbo].[countries]
ADD CONSTRAINT [PK_countries]
    PRIMARY KEY CLUSTERED ([country_id] ASC);
GO

-- Creating primary key on [emp_id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [PK_employees]
    PRIMARY KEY CLUSTERED ([emp_id] ASC);
GO

-- Creating primary key on [emp_cont_id] in table 'employee_contact'
ALTER TABLE [dbo].[employee_contact]
ADD CONSTRAINT [PK_employee_contact]
    PRIMARY KEY CLUSTERED ([emp_cont_id] ASC);
GO

-- Creating primary key on [holiday_id] in table 'holidays'
ALTER TABLE [dbo].[holidays]
ADD CONSTRAINT [PK_holidays]
    PRIMARY KEY CLUSTERED ([holiday_id] ASC);
GO

-- Creating primary key on [job_desc_id] in table 'job_description'
ALTER TABLE [dbo].[job_description]
ADD CONSTRAINT [PK_job_description]
    PRIMARY KEY CLUSTERED ([job_desc_id] ASC);
GO

-- Creating primary key on [leave_emp_id] in table 'leave_employee'
ALTER TABLE [dbo].[leave_employee]
ADD CONSTRAINT [PK_leave_employee]
    PRIMARY KEY CLUSTERED ([leave_emp_id] ASC);
GO

-- Creating primary key on [leave_id] in table 'leave_group'
ALTER TABLE [dbo].[leave_group]
ADD CONSTRAINT [PK_leave_group]
    PRIMARY KEY CLUSTERED ([leave_id] ASC);
GO

-- Creating primary key on [project_id] in table 'projects'
ALTER TABLE [dbo].[projects]
ADD CONSTRAINT [PK_projects]
    PRIMARY KEY CLUSTERED ([project_id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [task_id] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [PK_tasks]
    PRIMARY KEY CLUSTERED ([task_id] ASC);
GO

-- Creating primary key on [tracking_id] in table 'tracking_sheet'
ALTER TABLE [dbo].[tracking_sheet]
ADD CONSTRAINT [PK_tracking_sheet]
    PRIMARY KEY CLUSTERED ([tracking_id] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [company_id] in table 'calenders'
ALTER TABLE [dbo].[calenders]
ADD CONSTRAINT [FK_calender_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_calender_company'
CREATE INDEX [IX_FK_calender_company]
ON [dbo].[calenders]
    ([company_id]);
GO

-- Creating foreign key on [country_id] in table 'calenders'
ALTER TABLE [dbo].[calenders]
ADD CONSTRAINT [FK_calender_country]
    FOREIGN KEY ([country_id])
    REFERENCES [dbo].[countries]
        ([country_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_calender_country'
CREATE INDEX [IX_FK_calender_country]
ON [dbo].[calenders]
    ([country_id]);
GO

-- Creating foreign key on [holiday_id] in table 'calenders'
ALTER TABLE [dbo].[calenders]
ADD CONSTRAINT [FK_calender_holiday]
    FOREIGN KEY ([holiday_id])
    REFERENCES [dbo].[holidays]
        ([holiday_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_calender_holiday'
CREATE INDEX [IX_FK_calender_holiday]
ON [dbo].[calenders]
    ([holiday_id]);
GO

-- Creating foreign key on [client_id] in table 'companies'
ALTER TABLE [dbo].[companies]
ADD CONSTRAINT [FK_company_client]
    FOREIGN KEY ([client_id])
    REFERENCES [dbo].[clients]
        ([client_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_company_client'
CREATE INDEX [IX_FK_company_client]
ON [dbo].[companies]
    ([client_id]);
GO

-- Creating foreign key on [comp_id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [FK_employee_company]
    FOREIGN KEY ([comp_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_company'
CREATE INDEX [IX_FK_employee_company]
ON [dbo].[employees]
    ([comp_id]);
GO

-- Creating foreign key on [company_id] in table 'holidays'
ALTER TABLE [dbo].[holidays]
ADD CONSTRAINT [FK_holiday_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_holiday_company'
CREATE INDEX [IX_FK_holiday_company]
ON [dbo].[holidays]
    ([company_id]);
GO

-- Creating foreign key on [company_id] in table 'job_description'
ALTER TABLE [dbo].[job_description]
ADD CONSTRAINT [FK_job_description_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_job_description_company'
CREATE INDEX [IX_FK_job_description_company]
ON [dbo].[job_description]
    ([company_id]);
GO

-- Creating foreign key on [company_id] in table 'leave_group'
ALTER TABLE [dbo].[leave_group]
ADD CONSTRAINT [FK_leave_group_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_leave_group_company'
CREATE INDEX [IX_FK_leave_group_company]
ON [dbo].[leave_group]
    ([company_id]);
GO

-- Creating foreign key on [company_id] in table 'projects'
ALTER TABLE [dbo].[projects]
ADD CONSTRAINT [FK_project_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_project_company'
CREATE INDEX [IX_FK_project_company]
ON [dbo].[projects]
    ([company_id]);
GO

-- Creating foreign key on [company_id] in table 'tracking_sheet'
ALTER TABLE [dbo].[tracking_sheet]
ADD CONSTRAINT [FK_tracking_sheet_company]
    FOREIGN KEY ([company_id])
    REFERENCES [dbo].[companies]
        ([company_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tracking_sheet_company'
CREATE INDEX [IX_FK_tracking_sheet_company]
ON [dbo].[tracking_sheet]
    ([company_id]);
GO

-- Creating foreign key on [country_id] in table 'employee_contact'
ALTER TABLE [dbo].[employee_contact]
ADD CONSTRAINT [FK_employee_contact_country]
    FOREIGN KEY ([country_id])
    REFERENCES [dbo].[countries]
        ([country_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_contact_country'
CREATE INDEX [IX_FK_employee_contact_country]
ON [dbo].[employee_contact]
    ([country_id]);
GO

-- Creating foreign key on [country_id] in table 'holidays'
ALTER TABLE [dbo].[holidays]
ADD CONSTRAINT [FK_holiday_country]
    FOREIGN KEY ([country_id])
    REFERENCES [dbo].[countries]
        ([country_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_holiday_country'
CREATE INDEX [IX_FK_holiday_country]
ON [dbo].[holidays]
    ([country_id]);
GO

-- Creating foreign key on [employee_id] in table 'employee_contact'
ALTER TABLE [dbo].[employee_contact]
ADD CONSTRAINT [FK_employee_contact_employee]
    FOREIGN KEY ([employee_id])
    REFERENCES [dbo].[employees]
        ([emp_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_contact_employee'
CREATE INDEX [IX_FK_employee_contact_employee]
ON [dbo].[employee_contact]
    ([employee_id]);
GO

-- Creating foreign key on [holiday_id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [FK_employee_holiday]
    FOREIGN KEY ([holiday_id])
    REFERENCES [dbo].[holidays]
        ([holiday_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_holiday'
CREATE INDEX [IX_FK_employee_holiday]
ON [dbo].[employees]
    ([holiday_id]);
GO

-- Creating foreign key on [job_des_id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [FK_employee_job_desc]
    FOREIGN KEY ([job_des_id])
    REFERENCES [dbo].[job_description]
        ([job_desc_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employee_job_desc'
CREATE INDEX [IX_FK_employee_job_desc]
ON [dbo].[employees]
    ([job_des_id]);
GO

-- Creating foreign key on [approve_by_emp_id] in table 'leave_employee'
ALTER TABLE [dbo].[leave_employee]
ADD CONSTRAINT [FK_leave_employee_approved_by]
    FOREIGN KEY ([approve_by_emp_id])
    REFERENCES [dbo].[employees]
        ([emp_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_leave_employee_approved_by'
CREATE INDEX [IX_FK_leave_employee_approved_by]
ON [dbo].[leave_employee]
    ([approve_by_emp_id]);
GO

-- Creating foreign key on [employee_id] in table 'leave_employee'
ALTER TABLE [dbo].[leave_employee]
ADD CONSTRAINT [FK_leave_employee_employee]
    FOREIGN KEY ([employee_id])
    REFERENCES [dbo].[employees]
        ([emp_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_leave_employee_employee'
CREATE INDEX [IX_FK_leave_employee_employee]
ON [dbo].[leave_employee]
    ([employee_id]);
GO

-- Creating foreign key on [employee_id] in table 'tracking_sheet'
ALTER TABLE [dbo].[tracking_sheet]
ADD CONSTRAINT [FK_tracking_sheet_employee]
    FOREIGN KEY ([employee_id])
    REFERENCES [dbo].[employees]
        ([emp_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tracking_sheet_employee'
CREATE INDEX [IX_FK_tracking_sheet_employee]
ON [dbo].[tracking_sheet]
    ([employee_id]);
GO

-- Creating foreign key on [project_id] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [FK_task_project]
    FOREIGN KEY ([project_id])
    REFERENCES [dbo].[projects]
        ([project_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_task_project'
CREATE INDEX [IX_FK_task_project]
ON [dbo].[tasks]
    ([project_id]);
GO

-- Creating foreign key on [project_id] in table 'tracking_sheet'
ALTER TABLE [dbo].[tracking_sheet]
ADD CONSTRAINT [FK_tracking_sheet_project]
    FOREIGN KEY ([project_id])
    REFERENCES [dbo].[projects]
        ([project_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tracking_sheet_project'
CREATE INDEX [IX_FK_tracking_sheet_project]
ON [dbo].[tracking_sheet]
    ([project_id]);
GO

-- Creating foreign key on [tracking_sheet_id] in table 'tasks'
ALTER TABLE [dbo].[tasks]
ADD CONSTRAINT [FK_task_tracking_sheet]
    FOREIGN KEY ([tracking_sheet_id])
    REFERENCES [dbo].[tracking_sheet]
        ([tracking_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_task_tracking_sheet'
CREATE INDEX [IX_FK_task_tracking_sheet]
ON [dbo].[tasks]
    ([tracking_sheet_id]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------