create database PhamVanDongDB02
go
use PhamVanDongDB02
go
create table tblUserAccount
(
	Id			char(7),
	Username	varchar(50) unique,
	Password	varchar(50),
	Status		bit,
	constraint pk_tblUserAccount primary key(Id)
)
create table tblCategory
(
	Id			char(7),
	Name		nvarchar(50),
	Description	nvarchar(50),
	status		bit,
	constraint pk_tblDanhMucSP primary key(Id)
)
create table tblProduct
(
	Id				char(7),
	Name			nvarchar(50),
	UnitCost		money,
	Quantity		int,
	Image			nvarchar(100),
	Description		nvarchar(50),
	Status			bit,
	CategoryId		char(7),
	constraint pk_tblSanPham primary key(Id),
	constraint fk_tblSanPham_MaDM foreign key(CategoryId) references tblCategory(Id)
		on delete cascade
		on update cascade
)
go
create function FnDayso0
(
	@max int
)
returns varchar(5)
begin
	declare @dem int = 0, 
	@chuoi varchar(5) = ''
	--TH ma la NC00009 neu khong set max + 1 thi xuat chuoi 0000 khien ma tu dong 
	--tang tiep theo khong phai la NC00010 ma la NC00001
	set @max = @max + 1; 
	while @max <> 0
	begin
		set @dem = @dem + 1;
		set @max = @max/10;
	end
	while (5-@dem) > 0
	begin
		set @chuoi = @chuoi + '0';
		set @dem = @dem + 1;
	end
	return @chuoi
end
Go
create function fnAUTO_ID
( 
	@Ma char(2)
)
returns varchar(7)
begin
	declare @ID varchar(7)
	if @Ma='ID'
		if (select count(ID) from tblUserAccount) = 0
			set @ID = '0'
		else
			select @ID = max(right(ID, 5))  from tblUserAccount
	if @Ma='SP'
		if (select count(Id) from tblProduct) = 0
			set @ID = '0'
		else
			select @ID = max(right(Id, 5))  from tblProduct
	if @Ma='DM'
		if (select count(Id) from tblCategory) = 0
			set @ID = '0'
		else
			select @ID = max(right(Id, 5))  from tblCategory
	select @ID = @MA + dbo.fnDayso0(convert(int, @ID)) + convert(char, convert(int, @ID) + 1)
	return @ID
end
go
Insert into tblCategory
values	(dbo.fnAUTO_ID('DM'), 'Ram Laptop', 'Ram',1)
Insert into tblCategory
values	(dbo.fnAUTO_ID('DM'), N'Ổ cứng SSD', 'SSD',1)
Insert into tblCategory
values	(dbo.fnAUTO_ID('DM'), N'Ổ cứng HDD', 'HDD',1)
Insert into tblCategory
values	(dbo.fnAUTO_ID('DM'), 'USB', 'USB',1)

Insert into tblProduct(Id, Name, UnitCost, Quantity, Status, CategoryId, Image)
values	('SP00001', N'Ổ cứng SSD Samsung 250GB',850000,100, 1,'DM00002', 'ssdsamsung.png')
Insert into tblProduct(Id, Name, UnitCost, Quantity, Status, CategoryId, Image)
values (dbo.fnAUTO_ID('SP'), 'USB 16GB SanDisk', 120000,100,1,'DM00004', 'usb.jpg')
Insert into tblProduct(Id, Name, UnitCost, Quantity, Status, CategoryId, Image)
values (dbo.fnAUTO_ID('SP'), 'Ram Samsung 8GB',850000,100,1,'DM00001', 'ramlaptop.jpg')
Insert into tblProduct(Id, Name, UnitCost, Quantity, Status, CategoryId, Image)
values (dbo.fnAUTO_ID('SP'), 'Ram Laptop Samsung 16GB', 1600000, 100,1, 'DM00001', 'ramlaptop.jpg')

Insert into tblUserAccount
values (dbo.fnAUTO_ID('ID'), 'admin', '21232f297a57a5a743894a0e4a801fc3', 1)
Insert into tblUserAccount
values (dbo.fnAUTO_ID('ID'), 'user01', 'ee11cbb19052e40b07aac0ca060c23ee', 1)
Insert into tblUserAccount
values (dbo.fnAUTO_ID('ID'), 'user02', 'ee11cbb19052e40b07aac0ca060c23ee', 1)
Insert into tblUserAccount
values (dbo.fnAUTO_ID('ID'), 'user03', 'ee11cbb19052e40b07aac0ca060c23ee', 1)

