Create database ComputerServicesDB

/*
Created		21.11.2022
Modified		19.12.2022
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/


Drop table [Technic_type] 
go
Drop table [Brand] 
go
Drop table [Technic_tupe] 
go
Drop table [GPU] 
go
Drop table [RAM] 
go
Drop table [PSU] 
go
Drop table [Ìotherboard] 
go
Drop table [CPU] 
go
Drop table [Organization] 
go
Drop table [Table_part] 
go
Drop table [Employee] 
go
Drop table [Technic] 
go
Drop table [Contract] 
go
Drop table [Service] 
go
Drop table [Client] 
go


Create table [Client]
(
	[Id_Client] Integer Identity NOT NULL,
	[Client_name] Nvarchar(50) NOT NULL,
	[Surname_client] Nvarchar(50) NOT NULL,
	[Patronymic_client] Nvarchar(50) NULL,
	[Phone_number_client] Nvarchar(20) NOT NULL,
	[Address_client] Nvarchar(80) NOT NULL,
Primary Key ([Id_Client])
) 
go

Create table [Service]
(
	[Service_id] Integer Identity NOT NULL,
	[Service_name] Nvarchar(200) NOT NULL,
	[Service_description] Nvarchar(800) NULL,
	[Service_price] Integer NOT NULL,
	[Technic_type_id] Integer NOT NULL,
Primary Key ([Service_id])
) 
go

Create table [Contract]
(
	[Contract_id] Integer Identity NOT NULL,
	[Employee_id] Integer NOT NULL,
	[Id_Client] Integer NOT NULL,
	[Contract_date] Datetime NOT NULL,
	[Contract_discription] Nvarchar(4000) NOT NULL,
Primary Key ([Contract_id])
) 
go

Create table [Technic]
(
	[Technic_id] Integer Identity NOT NULL,
	[Technic_name] Nvarchar(200) NOT NULL,
	[CPU_id] Integer NOT NULL,
	[Motherboard_id] Integer NOT NULL,
	[PSU_id] Integer NOT NULL,
	[RAM_id] Integer NOT NULL,
	[GPU_id] Integer NOT NULL,
	[Technic_type_id] Integer NOT NULL,
Primary Key ([Technic_id])
) 
go

Create table [Employee]
(
	[Employee_id] Integer Identity NOT NULL,
	[Employee_name] Nvarchar(50) NOT NULL,
	[Employee_surname] Nvarchar(50) NOT NULL,
	[Employee_patronymic] Nvarchar(50) NULL,
	[Phone_number_employee] Nvarchar(30) NOT NULL,
	[Employee_INN] Integer NOT NULL,
Primary Key ([Employee_id])
) 
go

Create table [Table_part]
(
	[Entry_number] Integer NOT NULL,
	[Service_id] Integer NOT NULL,
	[Technic_id] Integer NOT NULL,
	[Contract_id] Integer NOT NULL,
Primary Key ([Entry_number])
) 
go

Create table [Organization]
(
	[Organization_type] Nvarchar(50) NOT NULL,
	[Organization_name] Nvarchar(150) NOT NULL,
	[Organization_address] Nvarchar(250) NOT NULL,
	[Organization_discription] Nvarchar(4000) NOT NULL
) 
go

Create table [CPU]
(
	[CPU_id] Integer Identity NOT NULL,
	[Brand_id] Integer NOT NULL,
	[CPU_model] Nvarchar(50) NOT NULL,
Primary Key ([CPU_id])
) 
go

Create table [Ìotherboard]
(
	[Motherboard_id] Integer Identity NOT NULL,
	[Chipset] Nvarchar(50) NOT NULL,
	[Socket] Nvarchar(50) NOT NULL,
	[Motherboard_brand] Nvarchar(50) NOT NULL,
	[Brand_id] Integer NOT NULL,
Primary Key ([Motherboard_id])
) 
go

Create table [PSU]
(
	[PSU_id] Integer Identity NOT NULL,
	[PSU_brand] Nvarchar(100) NOT NULL,
	[Power_PSU] Nvarchar(50) NOT NULL,
	[Brand_id] Integer NOT NULL,
Primary Key ([PSU_id])
) 
go

Create table [RAM]
(
	[RAM_id] Integer Identity NOT NULL,
	[RAM_name] Nvarchar(50) NOT NULL,
	[RAM_volume] Integer NOT NULL,
	[RAM_units] Integer NOT NULL,
	[Brand_id] Integer NOT NULL,
Primary Key ([RAM_id])
) 
go

Create table [GPU]
(
	[GPU_id] Integer Identity NOT NULL,
	[Name_GPU] Nvarchar(100) NOT NULL,
	[Brand_id] Integer NOT NULL,
Primary Key ([GPU_id])
) 
go

Create table [Technic_tupe]
(
	[Technic_type_id] Integer Identity NOT NULL,
	[Name_tt] Nvarchar(100) NOT NULL,
Primary Key ([Technic_type_id])
) 
go

Create table [Brand]
(
	[Brand_id] Integer Identity NOT NULL,
	[Brand_name] Nvarchar(100) NOT NULL,
Primary Key ([Brand_id])
) 
go

Create table [Technic_type]
(
	[Technic_type_id] Integer Identity NOT NULL,
	[Technic_type_name] Nvarchar(100) NOT NULL,
Primary Key ([Technic_type_id])
) 
go


Alter table [Contract] add  foreign key([Id_Client]) references [Client] ([Id_Client])  on update no action on delete no action 
go
Alter table [Table_part] add  foreign key([Service_id]) references [Service] ([Service_id])  on update no action on delete no action 
go
Alter table [Table_part] add  foreign key([Contract_id]) references [Contract] ([Contract_id])  on update no action on delete no action 
go
Alter table [Table_part] add  foreign key([Technic_id]) references [Technic] ([Technic_id])  on update no action on delete no action 
go
Alter table [Contract] add  foreign key([Employee_id]) references [Employee] ([Employee_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([CPU_id]) references [CPU] ([CPU_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([Motherboard_id]) references [Ìotherboard] ([Motherboard_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([PSU_id]) references [PSU] ([PSU_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([RAM_id]) references [RAM] ([RAM_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([GPU_id]) references [GPU] ([GPU_id])  on update no action on delete no action 
go
Alter table [Service] add  foreign key([Technic_type_id]) references [Technic_tupe] ([Technic_type_id])  on update no action on delete no action 
go
Alter table [Ìotherboard] add  foreign key([Brand_id]) references [Brand] ([Brand_id])  on update no action on delete no action 
go
Alter table [PSU] add  foreign key([Brand_id]) references [Brand] ([Brand_id])  on update no action on delete no action 
go
Alter table [RAM] add  foreign key([Brand_id]) references [Brand] ([Brand_id])  on update no action on delete no action 
go
Alter table [GPU] add  foreign key([Brand_id]) references [Brand] ([Brand_id])  on update no action on delete no action 
go
Alter table [CPU] add  foreign key([Brand_id]) references [Brand] ([Brand_id])  on update no action on delete no action 
go
Alter table [Technic] add  foreign key([Technic_type_id]) references [Technic_type] ([Technic_type_id])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


/* Roles permissions */


/* Users permissions */


