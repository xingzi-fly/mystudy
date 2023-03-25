use myhouse

--数据库建表
--（1）员工信息表
create table employee(
emp_ID	varchar	(10) not null,
emp_name	varchar	(20),
emp_sex	varchar	(10),
emp_birth	datetime,	
emp_phone	varchar	(20),
emp_cardID	varchar	(20),
emp_address	varchar	(50),
emp_govID	varchar	(10),
emp_studyID	varchar	(10),
emp_pay	numeric(19,4)
)

--（2）民族信息表
create table gov(
gov_ID	varchar	(10) not null,
gov_name	varchar	(20),
gov_remark	varchar	(50)
)

--（3）学历信息表
create table study(
study_ID	varchar	(10),
study_name	char	(20),
study_remark	varchar	(50)
)

--（4）客户信息表
create table users(
user_ID	varchar	(10) not null,
user_name	varchar	(20),
user_sex	varchar	(4),
user_birth	datetime,	
user_phone	varchar	(20),
user_cardID	varchar	(20),
user_type	varchar	(10),
house_ID	varchar	(10),
user_record	datetime	
)

--（5）房源信息表
create table house (
house_ID	varchar	(10) ,
house_company	varchar	(50),
house_typeID	varchar	(10),
house_state	varchar	(10),
house_fitID	varchar	(10),
house_favorID	varchar	(10),
house_mothedID	varchar	(10),
house_price		numeric(18,0),
house_floorID	varchar	(10),
house_buildYear	varchar	(10),
house_area	varchar	(20),
house_remark	varchar	(50),
user_ID	varchar	(10)
)

--（6）房型信息表
create table type(
type_ID	varchar	(10),
type_name	varchar	(20),
type_remark	varchar	(50)
)

--（7）装修信息表
create table fit(
fit_ID	varchar	(10) not null,
fit_name	varchar	(20),
fit_remark	varchar	(50)
)

--（8）朝向信息表
create table favor(
favor_ID	varchar	(10),
favor_name	varchar	(20),
favor_remark	varchar	(50)
)

--(9)用途信息表
create table method(
method_ID	varchar	(10),
method_name	varchar	(20),
method_remark	varchar	(50)
)

--（10）楼层信息表
create table floor(
floor_ID	varchar	(10)not null,
floor_name	varchar	(20),
floor_remark	varchar	(50)
)

--（11）求租意向表
create table intent(
intent_ID	varchar	(10) not null,
user_ID	varchar	(10),
intent_typeID	varchar	(10),
intent_fitID	varchar	(10),
intent_floorID	varchar	(10),
intent_favorID	varchar	(10),
intent_methodID	varchar	(10),
intent_price	numeric(19,4),
intent_area	  varchar	(20)
)

--（12）收费信息表
create table money(
money_ID	varchar	(10) not null,
money_pay	numeric(18,0),
emp_ID	varchar	(10),
emp_name	varchar	(20),
house_ID	varchar	(10),
money_time	varchar	(50),
money_remark	varchar	(100),
lend_ID	varchar	(10),
lend_name	varchar	(20),
lend_phone	varchar	(20),
want_ID	varchar	(10),
want_name	varchar	(20),
want_phone	varchar	(20)
)

--（13）登录信息表
create table login(
login_ID	varchar	(10) not null,
emp_ID	varchar	(10),
login_name	varchar	(20),
login_pwd	varchar	(15),
login_power	varchar	(10)
)



--数据库完整性
--（1）员工信息表
alter table employee add constraint e1 check (emp_sex in('男','女'))

--（3）学历信息表
alter table study add constraint s1 check (study_ID is not null)

--（4）客户信息表
alter table users add constraint u1 check (user_sex in('男','女'))
alter table users add constraint u2 check (user_type in('lend','want'))

--（5）房源信息表
alter table house add constraint h1 check (house_ID is not null)
alter table house add constraint h2 check (house_state in ('reserve','free','lent'))

--（6）房型信息表
alter table type add constraint t1 check (type_ID is not null)

--（8）朝向信息表
alter table favor  add constraint f1 check (favor_ID is not null)

--（9）用途信息表
alter table method add constraint m1 check (method_ID is not null)



--建立视图
go
create view employee_view
as
select
e1.emp_ID  员工编号,
e1.emp_name 姓名,
e1.emp_birth  出生日期,
e1.emp_phone  联系方式,
e1.emp_pay 工资,
o1.login_power 权限,
o1.login_pwd 密码
from employee e1,login o1
where e1.emp_ID=o1.emp_ID


go
create view house_view
as
select
h1.house_ID 房屋编号,
h1.house_company 小区名称,
h1.house_area 面积,
h1.house_price  价格,
f1.favor_name  朝向,
f2.fit_name  装修情况,
f3.floor_name  楼层,
m1.method_name 用途,
t1.type_name 类型,
h1.house_state 状态,
h1.user_ID 用户编号

from 
house h1,
favor f1,
fit f2,
floor f3,
method m1,
type t1

where 
h1.house_favorID=f1.favor_ID and
h1.house_fitID=f2.fit_ID and 
h1.house_floorID = f3.floor_ID and
h1.house_mothedID =m1.method_ID and
h1.house_typeID =t1.type_ID 





--触发器
--（1）员工信息表
go
create trigger tr1 on employee instead of insert
as
begin
declare
@emp_ID	varchar	(10) ,
@emp_name	varchar	(20),
@emp_sex	varchar	(10),
@emp_birth	datetime,	
@emp_phone	varchar	(20),
@emp_cardID	varchar	(20),
@emp_address	varchar	(50),
@emp_govID	varchar	(10),
@emp_studyID	varchar	(10),
@emp_pay	numeric(19,4)


select @emp_ID=Max(emp_ID) from employee
select @emp_name=emp_name from inserted
select @emp_sex=emp_sex from inserted
select @emp_birth=emp_birth from inserted
select @emp_phone=emp_phone	 from inserted
select @emp_cardID=emp_cardID from inserted
select @emp_address=emp_address from inserted
select @emp_govID	=emp_govID	 from inserted
select @emp_studyID=emp_studyID from inserted
select @emp_pay=emp_pay from inserted

	if(@emp_ID is null)
		set @emp_ID='emp1'
	else
		set @emp_ID='emp'+cast(cast(substring(@emp_ID,4,4) as int)+1 as varchar(20))

	insert into employee values(@emp_ID,@emp_name,@emp_sex,
					@emp_birth,@emp_phone,@emp_cardID,
					@emp_address,@emp_govID,@emp_studyID,@emp_pay)
end

--（2）民族信息表
go
create trigger tr2  on gov instead of insert
as
begin
	declare @gov_ID varchar(10),@gov_name varchar(20),@gov_remark varchar(50)
	select @gov_ID=Max(gov_ID) from gov
	select @gov_name=gov_name  from inserted
	select @gov_remark =gov_remark  from inserted
	if(@gov_ID is null)
		set @gov_ID='gov1'
	else
		set @gov_ID='gov'+cast((cast(substring(@gov_ID,4,4) as int)+1) as varchar(10))

	if exists(select gov_name from gov where gov_name=@gov_name)
		rollback
		
	else
		begin
			insert into gov values(@gov_ID,@gov_name,@gov_remark)			
		end
end

drop trigger tr2
--（3）学历信息表
go
create trigger tr3 on study instead of insert
as
begin
	declare @study_ID varchar(10),@study_name varchar(20),@study_remark varchar(50)
	select @study_ID=Max(study_ID) from study
	select @study_name=study_name  from inserted
	select @study_remark =study_remark  from inserted
	if(@study_ID is null)
		set @study_ID='study1'
	else
		set @study_ID='study'+cast((cast(substring(@study_ID,4,4) as int)+1) as varchar(10))

	if exists(select study_name from study where study_name=@study_name)
		rollback
		
	else
		begin
			insert into study values(@study_ID,@study_name,@study_remark)			
		end
end

--（4）客户信息表

go
create trigger tr10 on users instead of insert
as
begin
declare
@user_ID	varchar	(10) ,
@user_name	varchar	(20),
@user_sex	varchar	(10),
@user_birth	datetime,	
@user_phone	varchar	(20),
@user_cardID	varchar	(20),
@user_type varchar	(50),
@house_ID	varchar	(10),
@user_record	datetime	


select @user_ID=Max(user_ID) from users
select @user_name=user_name from inserted
select @user_sex=user_sex from inserted
select @user_birth=user_birth from inserted
select @user_phone=user_phone	 from inserted
select @user_cardID=user_cardID from inserted
select @user_type = user_type from inserted
select @house_ID	=house_ID	 from inserted
select @user_record =getdate()

	if(@user_type = 'lend')
begin
	if(@user_ID is null)
		set @user_ID='lend1'
	else
		set @user_ID='lend'+cast(cast(substring(@user_ID,5,4) as int)+1 as varchar(20))
end

	if(@user_type = 'want')
begin
	if(@user_ID is null)
		set @user_ID='want1'
	else
		set @user_ID='want'+cast(cast(substring(@user_ID,5,4) as int)+1 as varchar(20))
end

	insert into users values(@user_ID,@user_name,@user_sex,
					@user_birth,@user_phone,@user_cardID,
					@user_type,@house_ID,@user_record)
end

--drop trigger tr10
--（5）房源信息表
--drop trigger t11
go
create trigger t11 on house instead of insert
as
begin
declare
@house_ID	varchar	(10) ,
@house_company	varchar	(50),
@house_typeID	varchar	(10),
@house_state	varchar	(10),
@house_fitID	varchar	(10),
@house_favorID	 varchar	(10),
@house_methodID	varchar	(10),
@house_price		numeric(18,0),
@house_floorID	 varchar	(10),
@house_buildYear	varchar	(10),
@house_area	varchar	(20),
@house_remark	varchar	(50),
@user_ID	varchar	(10)

select @house_ID=(select Max(house_ID) from house)
select  @house_company= house_company from inserted
select  @house_typeID= house_typeID from inserted
select  @house_state= house_state from inserted
select  @house_fitID= house_fitID from inserted
select  @house_favorID= house_favorID from inserted
select @house_methodID= house_mothedID from inserted
select  @house_price= house_price from inserted
select  @house_floorID= house_floorID from inserted
select  @house_buildYear= house_buildYear from inserted
select  @house_area= house_area from inserted
select  @house_remark= house_remark from inserted
select  @user_ID = user_ID from inserted


	if(@house_ID is null)
		set @house_ID='hou1'
	else
		set @house_ID='hou'+cast(substring(@house_ID,4,4)+1 as varchar(10))
	
		insert into house values
				(@house_ID,
				@house_company,
				@house_typeID,
				@house_state,
				@house_fitID,
				@house_favorID,
				@house_methodID,
				@house_price,
				@house_floorID,
				@house_buildYear,
				@house_area,
				@house_remark,
				@user_ID )
end

--（6）房型信息表
go
create trigger tr4  on type instead of insert
as
begin
	declare @type_ID varchar(10),@type_name varchar(20),@type_remark varchar(50)
	select @type_ID=Max(type_ID) from type
	select @type_name=type_name  from inserted
	select @type_remark =type_remark  from inserted
	if(@type_ID is null)
		set @type_ID='type1'
	else
		set @type_ID='type'+cast((cast(substring(@type_ID,4,4) as int)+1) as varchar(10))

	if exists(select type_name from type where type_name=@type_name)
		rollback
		
	else
		begin
			insert into type values(@type_ID,@type_name,@type_remark)			
		end
end

--（7）装修信息表
go
create trigger tr5  on fit instead of insert
as
begin
	declare @fit_ID varchar(10),@fit_name varchar(20),@fit_remark varchar(50)
	select @fit_ID=Max(fit_ID) from fit
	select @fit_name=fit_name  from inserted
	select @fit_remark =fit_remark  from inserted
	if(@fit_ID is null)
		set @fit_ID='fit1'
	else
		set @fit_ID='fit'+cast((cast(substring(@fit_ID,4,4) as int)+1) as varchar(10))

	if exists(select fit_name from fit where fit_name=@fit_name)
		rollback
		
	else
		begin
			insert into fit values(@fit_ID,@fit_name,@fit_remark)			
		end
end

--（8）朝向信息表
go
create trigger tr6  on favor instead of insert
as
begin
	declare @favor_ID varchar(10),@favor_name varchar(20),@favor_remark varchar(50)
	select @favor_ID=Max(favor_ID) from favor
	select @favor_name=favor_name  from inserted
	select @favor_remark =favor_remark  from inserted
	if(@favor_ID is null)
		set @favor_ID='favor1'
	else
		set @favor_ID='favor'+cast((cast(substring(@favor_ID,4,4) as int)+1) as varchar(10))

	if exists(select favor_name from favor where favor_name=@favor_name)
		rollback
		
	else
		begin
			insert into favor values(@favor_ID,@favor_name,@favor_remark)			
		end
end

--(9)用途信息表
go
create trigger tr7  on method instead of insert
as
begin
	declare @method_ID varchar(10),@method_name varchar(20),@method_remark varchar(50)
	select @method_ID=Max(method_ID) from method
	select @method_name=method_name  from inserted
	select @method_remark =method_remark  from inserted
	if(@method_ID is null)
		set @method_ID='method1'
	else
		set @method_ID='method'+cast((cast(substring(@method_ID,4,4) as int)+1) as varchar(10))

	if exists(select method_name from method where method_name=@method_name)
		rollback
		
	else
		begin
			insert into method values(@method_ID,@method_name,@method_remark)			
		end
end


--（10）楼层信息表
go
create trigger tr7  on floor instead of insert
as
begin
	declare @floor_ID varchar(10),@floor_name varchar(20),@floor_remark varchar(50)
	select @floor_ID=Max(floor_ID) from floor
	select @floor_name=floor_name  from inserted
	select @floor_remark =floor_remark  from inserted
	if(@floor_ID is null)
		set @floor_ID='floor1'
	else
		set @floor_ID='floor'+cast((cast(substring(@floor_ID,4,4) as int)+1) as varchar(10))

	if exists(select floor_name from floor where floor_name=@floor_name)
		rollback
		
	else
		begin
			insert into floor values(@floor_ID,@floor_name,@floor_remark)			
		end
end
--（11）求租意向表
go
create trigger t12 on intent instead of insert
as
begin
declare
@intent_ID	varchar	(10) ,
@user_ID	varchar	(50),
@intent_typeID	varchar	(10),
@intent_fitID	varchar	(10),
@intent_favorID	 varchar	(10),
@intent_floorID	 varchar	(10),
@intent_methodID	varchar	(10),
@intent_price		numeric(18,0),
@intent_area	varchar	(20)

select @intent_ID=(select Max(intent_ID) from intent)
select @user_ID = user_ID from inserted
select @intent_typeID= intent_typeID from inserted
select @intent_fitID= intent_fitID from inserted
select @intent_favorID= intent_favorID from inserted
select @intent_floorID= intent_floorID from inserted
select @intent_methodID = intent_methodID from inserted
select @intent_price= intent_price from inserted
select @intent_area= intent_area from inserted


	if(@intent_ID is null)
		set @intent_ID='int1'
	else
		set @intent_ID='int'+cast(substring(@intent_ID,4,4)+1 as varchar(10))
				
		insert into intent values
				(@intent_ID,
				@user_ID,
				@intent_typeID,
				@intent_fitID,
				@intent_favorID,
				@intent_floorID,
				@intent_methodID,
				@intent_price,
				@intent_area
				)
end


--（12）收费信息表
go
create trigger t13 on money instead of insert
as
begin
declare
@money_ID	varchar	(10) ,
@money_pay	numeric(18,0),
@emp_ID	varchar	(10),
@emp_name	varchar	(20),
@house_ID	varchar	(10),
@money_time	varchar	(50),
@money_remark	varchar	(100),
@lend_ID	varchar	(10),
@lend_name	varchar	(20),
@lend_phone	varchar	(20),
@want_ID	varchar	(10),
@want_name	varchar	(20),
@want_phone	varchar	(20)


select @money_ID=(select Max(money_ID) from money)
select @money_pay = money_pay from inserted
select @emp_ID = emp_ID from inserted
select @emp_name = emp_name from inserted
select @house_ID = house_ID from inserted
select @money_time = money_time from inserted
select @money_remark = money_remark from inserted

select @lend_ID = lend_ID from inserted
select @lend_name = lend_name from inserted
select @lend_phone = lend_phone from inserted
select @want_ID	= want_ID	 from inserted
select @want_name = want_name from inserted
select @want_phone = want_phone from inserted



	if(@money_ID is null)
		set @money_ID='money1'
	else
		set @money_ID='money'+cast(substring(@money_ID,6,4)+1 as varchar(10))
				
		insert into money values(
		@money_ID	,
		@money_pay	,
		@emp_ID	,
		@emp_name	,
		@house_ID	,
		@money_time	,
		@money_remark	,
		@lend_ID	,
		@lend_name	,
		@lend_phone	,
		@want_ID	,
		@want_name	,
		@want_phone	
)
end


--（13）登录信息表
go
create trigger t14 on login instead of insert
as
begin
	declare 
@login_ID varchar(10),
@emp_ID	varchar	(10),
@login_name	varchar	(20),
@login_pwd	varchar	(15),
@login_power	varchar	(10)

	select @login_ID=Max(login_ID) from login
	select @emp_ID = emp_ID  from inserted
	select @login_name=login_name from inserted
	select @login_pwd	= login_pwd	from inserted
	select @login_power = login_power from inserted
	


	if(@login_ID is null)
		set @login_ID='log1001' 
	else
		set @login_ID='log'+cast(cast(substring(@login_ID,4,4) as int)+1 as varchar(20))

	if exists(select login_name from login where login_name=@login_name)
		rollback 
		
	else
		begin
			insert into login values(@login_ID, @emp_ID	,@login_name,@login_pwd, @login_power)			
		end
end




--插入语句
--（1）员工信息表
insert into employee(
emp_name,
emp_sex,
emp_birth,
emp_phone,	
emp_cardID,
emp_address,
emp_govID,	
emp_studyID ,
emp_pay
)
values(
'mr','男',	'2001-11-15 00:00:00.000',	'13652412345',
'101010222000101014','汤臣一品','gov1','stu1'	,2000.0000
)

--（2）民族信息
insert into gov(gov_name,gov_remark) values('汉族','人数最多')
insert into gov(gov_name,gov_remark) values('回族','不吃猪肉')

--（4）客户信息表
insert into users
(user_name,
user_sex,
user_birth,
user_phone,
user_cardID,
user_type,
house_ID
)

values
('阿松大','男','1991-11-15 00:00:00.000','13255525552','152452633996874152','lend','hou1')

insert into users
(user_name,
user_sex,
user_birth,
user_phone,
user_cardID,
user_type
)

values
('大王','男','1998-8-15 20:00:00.000','1254478965','325145215478965415','want')
--函数
--补0
go
create function zh(@p varchar(4))
returns varchar(2)
as
begin
  if len(@p)<4
    begin
      set @p='0'+@p
    end
  return @p
end

--索引
create unique index q1 on employee(emp_ID asc)
create unique index q2 on favor(favor_ID asc)
create unique index q3 on fit(fit_ID asc)
create unique index q4 on floor(floor_ID asc)
create unique index q5 on gov(gov_ID asc)
create unique index q6 on house(house_ID asc)
create unique index q7 on intent(intent_ID asc)
create unique index q8 on login(login_ID asc)
create unique index q9 on method(method_ID asc)
create unique index q10 on money(money_ID asc)
create unique index q11 on study(study_ID asc)
create unique index q12 on type(type_ID asc)
create unique index q13 on users(user_ID asc)