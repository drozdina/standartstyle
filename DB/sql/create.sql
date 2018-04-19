if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ATTRIBUTES') and o.name = 'FK_ATTRIBUT_REFERENCE_GOODS_CA')
alter table ATTRIBUTES
   drop constraint FK_ATTRIBUT_REFERENCE_GOODS_CA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CBD_COLLECTION') and o.name = 'FK_CBD_COLL_REFERENCE_CBD_MANU')
alter table CBD_COLLECTION
   drop constraint FK_CBD_COLL_REFERENCE_CBD_MANU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CBD_COLOR') and o.name = 'FK_CBD_COLO_REFERENCE_CBD_COLL')
alter table CBD_COLOR
   drop constraint FK_CBD_COLO_REFERENCE_CBD_COLL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CBD_COLOR') and o.name = 'FK_CBD_COLO_REFERENCE_IMAGE')
alter table CBD_COLOR
   drop constraint FK_CBD_COLO_REFERENCE_IMAGE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOODS') and o.name = 'FK_GOODS_REFERENCE_GOODS_CA')
alter table GOODS
   drop constraint FK_GOODS_REFERENCE_GOODS_CA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOODS') and o.name = 'FK_GOODS_REFERENCE_USERS')
alter table GOODS
   drop constraint FK_GOODS_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOOD_ATTRIBUTES') and o.name = 'FK_GOOD_ATT_REFERENCE_GOODS')
alter table GOOD_ATTRIBUTES
   drop constraint FK_GOOD_ATT_REFERENCE_GOODS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOOD_ATTRIBUTES') and o.name = 'FK_GOOD_ATT_REFERENCE_ATTRIBUT')
alter table GOOD_ATTRIBUTES
   drop constraint FK_GOOD_ATT_REFERENCE_ATTRIBUT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOOD_COLOR') and o.name = 'FK_GOOD_COL_REFERENCE_GOODS')
alter table GOOD_COLOR
   drop constraint FK_GOOD_COL_REFERENCE_GOODS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOOD_COLOR') and o.name = 'FK_GOOD_COL_REFERENCE_CBD_COLO')
alter table GOOD_COLOR
   drop constraint FK_GOOD_COL_REFERENCE_CBD_COLO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IMAGE') and o.name = 'FK_IMAGE_REFERENCE_GOODS')
alter table IMAGE
   drop constraint FK_IMAGE_REFERENCE_GOODS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IMAGE') and o.name = 'FK_IMAGE_REFERENCE_USERS')
alter table IMAGE
   drop constraint FK_IMAGE_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REPLY') and o.name = 'FK_REPLY_REFERENCE_USERS')
alter table REPLY
   drop constraint FK_REPLY_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REPLY') and o.name = 'FK_REPLY_REFERENCE_GOODS')
alter table REPLY
   drop constraint FK_REPLY_REFERENCE_GOODS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERS') and o.name = 'FK_USERS_REFERENCE_ROLES')
alter table USERS
   drop constraint FK_USERS_REFERENCE_ROLES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ATTRIBUTES')
            and   type = 'U')
   drop table ATTRIBUTES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CBD_COLLECTION')
            and   type = 'U')
   drop table CBD_COLLECTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CBD_COLOR')
            and   type = 'U')
   drop table CBD_COLOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CBD_MANUFACTURER')
            and   type = 'U')
   drop table CBD_MANUFACTURER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GOODS')
            and   type = 'U')
   drop table GOODS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GOODS_CATEGORY')
            and   type = 'U')
   drop table GOODS_CATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GOOD_ATTRIBUTES')
            and   type = 'U')
   drop table GOOD_ATTRIBUTES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GOOD_COLOR')
            and   type = 'U')
   drop table GOOD_COLOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IMAGE')
            and   type = 'U')
   drop table IMAGE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('REPLY')
            and   type = 'U')
   drop table REPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ROLES')
            and   type = 'U')
   drop table ROLES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USERS')
            and   type = 'U')
   drop table USERS
go

/*==============================================================*/
/* Table: ATTRIBUTES                                            */
/*==============================================================*/
create table ATTRIBUTES (
   ATTRIBUTECODE        integer              identity,
   CATEGORYCODE         integer              null,
   ATTRIBUTENAME        varchar(max)         null,
   constraint PK_ATTRIBUTES primary key (ATTRIBUTECODE)
)
go

/*==============================================================*/
/* Table: CBD_COLLECTION                                        */
/*==============================================================*/
create table CBD_COLLECTION (
   COLLECTIONCODE       integer              identity,
   MANCODE              integer              null,
   NAME                 varchar(255)         null,
   constraint PK_CBD_COLLECTION primary key (COLLECTIONCODE)
)
go

/*==============================================================*/
/* Table: CBD_COLOR                                             */
/*==============================================================*/
create table CBD_COLOR (
   COLORCODE            integer              identity,
   COLLECTIONCODE       integer              null,
   IMAGECODE            integer              null,
   NAME                 varchar(255)         null,
   constraint PK_CBD_COLOR primary key (COLORCODE)
)
go

/*==============================================================*/
/* Table: CBD_MANUFACTURER                                      */
/*==============================================================*/
create table CBD_MANUFACTURER (
   MANCODE              integer              identity,
   NAME                 varchar(255)         null,
   COUNTRY              varchar(255)         null,
   constraint PK_CBD_MANUFACTURER primary key (MANCODE)
)
go

/*==============================================================*/
/* Table: GOODS                                                 */
/*==============================================================*/
create table GOODS (
   GOODCODE             integer              identity,
   CATEGORYCODE         integer              null,
   USERCODE             integer              null,
   NAME                 varchar(50)          null,
   DESCRIPTION          text                 null,
   STATE                integer              null,
   WIDTH                varchar(15)          null,
   HEIGHT               varchar(15)          null,
   DEPTH                varchar(15)          null,
   constraint PK_GOODS primary key (GOODCODE)
)
go

/*==============================================================*/
/* Table: GOODS_CATEGORY                                        */
/*==============================================================*/
create table GOODS_CATEGORY (
   CATEGORYCODE         integer              identity,
   NAME                 varchar(50)          null,
   constraint PK_GOODS_CATEGORY primary key (CATEGORYCODE)
)
go

/*==============================================================*/
/* Table: GOOD_ATTRIBUTES                                       */
/*==============================================================*/
create table GOOD_ATTRIBUTES (
   GATCODE              integer              identity,
   GOODCODE             integer              null,
   ATTRIBUTECODE        integer              null,
   constraint PK_GOOD_ATTRIBUTES primary key (GATCODE)
)
go

/*==============================================================*/
/* Table: GOOD_COLOR                                            */
/*==============================================================*/
create table GOOD_COLOR (
   GCCODE               integer              identity,
   COLORCODE            integer              null,
   GOODCODE             integer              null,
   constraint PK_GOOD_COLOR primary key (GCCODE)
)
go

/*==============================================================*/
/* Table: IMAGE                                                 */
/*==============================================================*/
create table IMAGE (
   IMAGECODE            integer              identity,
   USERCODE             integer              null,
   GOODCODE             integer              null,
   NAME                 varchar(50)          null,
   LOCATION             varchar(max)         null,
   UPLOAD_DATE          datetime             null,
   IS_MAIN              bit                  null,
   EXTENSION            varchar(10)          null,
   constraint PK_IMAGE primary key (IMAGECODE)
)
go

/*==============================================================*/
/* Table: REPLY                                                 */
/*==============================================================*/
create table REPLY (
   REPLYCODE            integer              identity,
   GOODCODE             integer              null,
   USERCODE             integer              null,
   AUTHOR               varchar(255)         null,
   REPLY_DATE           datetime             null,
   REPLY_TEXT           text                 null,
   AUTHOR_LOCATION      varchar(255)         null,
   STATE                integer              null,
   constraint PK_REPLY primary key (REPLYCODE)
)
go

/*==============================================================*/
/* Table: ROLES                                                 */
/*==============================================================*/
create table ROLES (
   ROLECODE             integer              not null,
   ROLENAME             varchar(50)          null,
   constraint PK_ROLES primary key (ROLECODE)
)
go

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
create table USERS (
   USERCODE             integer              identity,
   ROLECODE             integer              null,
   USERNAME             varchar(50)          null,
   PASSWORD             varchar(50)          null,
   constraint PK_USERS primary key (USERCODE)
)
go

alter table ATTRIBUTES
   add constraint FK_ATTRIBUT_REFERENCE_GOODS_CA foreign key (CATEGORYCODE)
      references GOODS_CATEGORY (CATEGORYCODE)
go

alter table CBD_COLLECTION
   add constraint FK_CBD_COLL_REFERENCE_CBD_MANU foreign key (MANCODE)
      references CBD_MANUFACTURER (MANCODE)
go

alter table CBD_COLOR
   add constraint FK_CBD_COLO_REFERENCE_CBD_COLL foreign key (COLLECTIONCODE)
      references CBD_COLLECTION (COLLECTIONCODE)
go

alter table CBD_COLOR
   add constraint FK_CBD_COLO_REFERENCE_IMAGE foreign key (IMAGECODE)
      references IMAGE (IMAGECODE)
go

alter table GOODS
   add constraint FK_GOODS_REFERENCE_GOODS_CA foreign key (CATEGORYCODE)
      references GOODS_CATEGORY (CATEGORYCODE)
go

alter table GOODS
   add constraint FK_GOODS_REFERENCE_USERS foreign key (USERCODE)
      references USERS (USERCODE)
go

alter table GOOD_ATTRIBUTES
   add constraint FK_GOOD_ATT_REFERENCE_GOODS foreign key (GOODCODE)
      references GOODS (GOODCODE)
go

alter table GOOD_ATTRIBUTES
   add constraint FK_GOOD_ATT_REFERENCE_ATTRIBUT foreign key (ATTRIBUTECODE)
      references ATTRIBUTES (ATTRIBUTECODE)
go

alter table GOOD_COLOR
   add constraint FK_GOOD_COL_REFERENCE_GOODS foreign key (GOODCODE)
      references GOODS (GOODCODE)
go

alter table GOOD_COLOR
   add constraint FK_GOOD_COL_REFERENCE_CBD_COLO foreign key (COLORCODE)
      references CBD_COLOR (COLORCODE)
go

alter table IMAGE
   add constraint FK_IMAGE_REFERENCE_GOODS foreign key (GOODCODE)
      references GOODS (GOODCODE)
go

alter table IMAGE
   add constraint FK_IMAGE_REFERENCE_USERS foreign key (USERCODE)
      references USERS (USERCODE)
go

alter table REPLY
   add constraint FK_REPLY_REFERENCE_USERS foreign key (USERCODE)
      references USERS (USERCODE)
go

alter table REPLY
   add constraint FK_REPLY_REFERENCE_GOODS foreign key (GOODCODE)
      references GOODS (GOODCODE)
go

alter table USERS
   add constraint FK_USERS_REFERENCE_ROLES foreign key (ROLECODE)
      references ROLES (ROLECODE)
go

insert into GOODS_CATEGORY (NAME) values ('Столы');
insert into GOODS_CATEGORY (NAME) values ('Комоды');
insert into GOODS_CATEGORY (NAME) values ('Тумбы TV');
insert into GOODS_CATEGORY (NAME) values ('Шкафы');
insert into GOODS_CATEGORY (NAME) values ('Кровати');
insert into GOODS_CATEGORY (NAME) values ('Горки');
insert into GOODS_CATEGORY (NAME) values ('Прихожие');

insert into ROLES (ROLECODE, ROLENAME) values (1, 'Admin');
insert into ROLES (ROLECODE, ROLENAME) values (2, 'User');

INSERT INTO USERS (ROLECODE, USERNAME, PASSWORD) VALUES (1, 'admin', 'fIdUH9Pz71AW4S1BGQDIemBGqOg=');

insert into ATTRIBUTES (CATEGORYCODE, ATTRIBUTENAME) VALUES (1, 'Компьютерный');
insert into ATTRIBUTES (CATEGORYCODE, ATTRIBUTENAME) VALUES (1, 'С надставкой');
insert into ATTRIBUTES (CATEGORYCODE, ATTRIBUTENAME) VALUES (1, 'С полками');
insert into ATTRIBUTES (CATEGORYCODE, ATTRIBUTENAME) VALUES (1, 'Письменный');
insert into ATTRIBUTES (CATEGORYCODE, ATTRIBUTENAME) VALUES (1, 'Стол-стеллаж');