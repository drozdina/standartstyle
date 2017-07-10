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
   where r.fkeyid = object_id('GOODS') and o.name = 'FK_GOODS_REFERENCE_IMAGE')
alter table GOODS
   drop constraint FK_GOODS_REFERENCE_IMAGE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GOODS') and o.name = 'FK_GOODS_REFERENCE_USERS')
alter table GOODS
   drop constraint FK_GOODS_REFERENCE_USERS
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
   where r.fkeyid = object_id('IMAGE') and o.name = 'FK_IMAGE_REFERENCE_USERS')
alter table IMAGE
   drop constraint FK_IMAGE_REFERENCE_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REPLY') and o.name = 'FK_REPLY_REFERENCE_GOODS')
alter table REPLY
   drop constraint FK_REPLY_REFERENCE_GOODS
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
           where  id = object_id('USERS')
            and   type = 'U')
   drop table USERS
go

/*==============================================================*/
/* Table: CBD_COLLECTION                                        */
/*==============================================================*/
create table CBD_COLLECTION (
   COLLECTIONCODE       integer              not null,
   MANCODE              integer              null,
   NAME                 varchar(255)         null,
   constraint PK_CBD_COLLECTION primary key (COLLECTIONCODE)
)
go

/*==============================================================*/
/* Table: CBD_COLOR                                             */
/*==============================================================*/
create table CBD_COLOR (
   COLORCODE            integer              not null,
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
   MANCODE              integer              not null,
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
   IMAGECODE            integer              null,
   USERCODE             integer              null,
   NAME                 varchar(50)          null,
   DESCRIPTION          text                 null,
   STATE                integer              null,
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
/* Table: GOOD_COLOR                                            */
/*==============================================================*/
create table GOOD_COLOR (
   GCCODE               integer              not null,
   COLORCODE            integer              null,
   GOODCODE             integer              null,
   constraint PK_GOOD_COLOR primary key (GCCODE)
)
go

/*==============================================================*/
/* Table: IMAGE                                                 */
/*==============================================================*/
create table IMAGE (
   IMAGECOED            integer              identity,
   USERCODE             integer              null,
   NAME                 varchar(50)          null,
   LOCATION             varchar(max)         null,
   UPLOAD_DATE          datetime             null,
   IS_MAIN              bit                  null,
   constraint PK_IMAGE primary key (IMAGECOED)
)
go

/*==============================================================*/
/* Table: REPLY                                                 */
/*==============================================================*/
create table REPLY (
   REPLYCODE            integer              not null,
   GOODCODE             integer              null,
   AUTHOR               varchar(255)         null,
   REPLY_DATE           datetime             null,
   REPLY_TEXT           text                 null,
   AUTHOR_LOCATION      varchar(255)         null,
   STATE                integer              null,
   constraint PK_REPLY primary key (REPLYCODE)
)
go

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
create table USERS (
   USERCODE             integer              not null,
   UERNAME              varchar(50)          null,
   PASSWORD             varchar(50)          null,
   constraint PK_USERS primary key (USERCODE)
)
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
      references IMAGE (IMAGECOED)
go

alter table GOODS
   add constraint FK_GOODS_REFERENCE_GOODS_CA foreign key (CATEGORYCODE)
      references GOODS_CATEGORY (CATEGORYCODE)
go

alter table GOODS
   add constraint FK_GOODS_REFERENCE_IMAGE foreign key (IMAGECODE)
      references IMAGE (IMAGECOED)
go

alter table GOODS
   add constraint FK_GOODS_REFERENCE_USERS foreign key (USERCODE)
      references USERS (USERCODE)
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
   add constraint FK_IMAGE_REFERENCE_USERS foreign key (USERCODE)
      references USERS (USERCODE)
go

alter table REPLY
   add constraint FK_REPLY_REFERENCE_GOODS foreign key (GOODCODE)
      references GOODS (GOODCODE)
go
