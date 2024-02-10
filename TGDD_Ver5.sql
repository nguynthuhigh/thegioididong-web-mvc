use master
if exists (select * from sysdatabases where name = 'TGDD_Ver5')
	drop database TGDD_Ver5
create database TGDD_Ver5
go
use TGDD_Ver5
CREATE TABLE BANNER(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	NameBanner NVARCHAR(Max),
	ImgBanner NVARCHAR(Max),
	LinkBanner NVARCHAR(Max),
	Color_BGNVARCHAR NVARCHAR(Max)
)
CREATE TABLE MostSearched(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	Keyword NVARCHAR(Max),
	Point INT,
)
CREATE TABLE Category (
    IDCate INT PRIMARY KEY NOT NULL,
    NameCate NVARCHAR(250) NULL,
);
CREATE TABLE Customer(
	Point INT,
	Phone	INT PRIMARY KEY,
	Gender		NVARCHAR (10),
    NameUser    NVARCHAR (250),
    PasswordUser NCHAR (50),
	AddressUser NVARCHAR (250)
)
CREATE TABLE TradeMark(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	NameTM NVARCHAR(250),
	Logo NVARCHAR(MAX),
	Descript NVARCHAR(250)
)
CREATE TABLE Color(
	ColorID INT IDENTITY (1, 1) NOT NULL,
	ColorName Nvarchar(100),
	RGB NCHAR(10) NOT NULL,
	PRIMARY KEY CLUSTERED ([ColorID] ASC)
)
CREATE TABLE Bonus(
	BonusID INT IDENTITY (1, 1) NOT NULL,
	Bonus_Name NVARCHAR(MAX),
	Img_Bonus NVARCHAR(MAX),
	RGB NCHAR(10) NOT NULL,
	RGB_2 NCHAR(10) NOT NULL,
	PRIMARY KEY CLUSTERED (BonusID ASC)
)

CREATE TABLE Product(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	NamePro NVARCHAR(MAX),
	ImagePro NVARCHAR(MAX),
	ImageDetail NVARCHAR(MAX),
	Installment INT,
	Title_Discount NVARCHAR(MAX),
	Info NTEXT,
	Promotion NTEXT,
	PromoAdd NTEXT,
	PolicyPro NTEXT,
	Screen NVARCHAR(MAX),
	Type_Product NVARCHAR(MAX),
	Graphics_Card NVARCHAR(MAX),
	Connector NVARCHAR(MAX),
	OS NVARCHAR(MAX),
	Design NVARCHAR(MAX),
	Size NVARCHAR(MAX),
	Mass NVARCHAR(MAX),
	Special NVARCHAR(MAX),
	CameraSau NVARCHAR(MAX),
	CameraTruoc NVARCHAR(MAX),
	Chip NVARCHAR(MAX),
	Sim NVARCHAR(MAX),
	Pin NVARCHAR(MAX),
	DateRelease INT,
	CategoryID INT,
	TradeMarkID INT,
	Rating DECIMAL(10, 1),
	PromotionID INT,
	PolicyProID INT,
	ServiceID INT,
	CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category (IDCate),
	CONSTRAINT FK_Product_TradeMark FOREIGN KEY (TradeMarkID) REFERENCES TradeMark (ID)
)
CREATE TABLE ProDetail(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ProID INT,
	Discount INT NULL DEFAULT 0,
	HardDrive NVARCHAR(MAX),
	RAM NVARCHAR(MAX),
	CPU NVARCHAR(MAX),
	Price INT NOT NULL,
	Old_Price INT NOT NULL,
	ColorID INT ,
	BonusID INT ,
	SoldQuantity INT NULL DEFAULT 0,
	ViewQuantity INT NULL DEFAULT 0,
	CONSTRAINT FK_ProDetail_Color FOREIGN KEY (ColorID) REFERENCES Color (ColorID),
	CONSTRAINT FK_ProDetail_Bonus FOREIGN KEY (BonusID) REFERENCES Bonus (BonusID),
	CONSTRAINT FK_ProDetail_Product FOREIGN KEY (ProID) REFERENCES Product (ID)
)
CREATE TABLE Comment(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ImgCMT Nvarchar(MAX),
	ID_Pro INT,
	PhoneCus INT,
	Content NTEXT,
	DateCMT DATE,
	Rating INT,
	CONSTRAINT FK_Comment_Customer FOREIGN KEY (PhoneCus) REFERENCES Customer (Phone),
	CONSTRAINT FK_Comment_Product FOREIGN KEY (ID_Pro) REFERENCES Product (ID)
)
CREATE TABLE ListImage(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ProID INT,
	CmtID INT,
	ImagePro NVARCHAR(MAX),
	CONSTRAINT FK_ListImage_Product FOREIGN KEY (ProID) REFERENCES Product (ID),
	CONSTRAINT FK_ListImage_Comment FOREIGN KEY (CmtID) REFERENCES Comment (ID)
)
CREATE TABLE ListImage_2(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ProID INT,
	CmtID INT,
	ImagePro NVARCHAR(MAX),
	CONSTRAINT FK_ListImage_2_ProDetail FOREIGN KEY (ProID) REFERENCES ProDetail (ID),
)
CREATE TABLE Voucher(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	NameVoucher NVARCHAR(250),
	Discount INT,
	Descript NVARCHAR(250),
	MinPro INT,
	MinTotal INT,
)
CREATE TABLE Cart(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	Phone_Cus INT,
	VoucherID INT,
	Point INT,
	Total_Temp INT,
	Total_Price INT,
	CONSTRAINT FK_Cart_Customer FOREIGN KEY (Phone_Cus) REFERENCES Customer (Phone)
);
CREATE TABLE CartDetail(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ID_Pro INT ,
	ID_Cart INT,
	Price INT,
	Quantity INT,
	Total INT,
	CONSTRAINT FK_CartDetail_Cart FOREIGN KEY (ID_Cart) REFERENCES Cart (ID),
	CONSTRAINT FK_CartDetail_Products FOREIGN KEY (ID_Pro) REFERENCES ProDetail (ID),
)
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY (1, 1),
    Oder_ID INT,
    PaymentAmount INT NOT NULL,
    PaymentDate DATETIME NOT NULL,
    PaymentMethod VARCHAR(MAX) NOT NULL,
   
);
CREATE TABLE OrderPro(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ID_Pro INT,
	PhoneCus INT,
	Total_Temp INT,
	Point INT,
	Total_Price INT,
	Date_Order DATE,
	Deadline DATE,
	Status_Order BIT,
	ID_Voucher INT,
	AddressCus NVARCHAR(MAX),
	PaymentID INT,
	ID_Cart INT,
	CONSTRAINT FK_OrderPro_Customer FOREIGN KEY (PhoneCus) REFERENCES Customer (Phone) ,
	CONSTRAINT FK_OrderPro_Cart FOREIGN KEY (ID_Cart) REFERENCES Cart (ID) ,
	CONSTRAINT FK_OrderPro_Payment FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
	CONSTRAINT FK_OrderPro_Voucher FOREIGN KEY (ID_Voucher) REFERENCES Voucher(ID),
)
CREATE TABLE OrderDetail(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	ID_Pro INT,
	ID_Order INT,
	Price INT,
	Quantity INT,
	Total INT,
	Status_Order BIT,
	Deadline DATE,
	CONSTRAINT FK_OrderDetail_ProDetail FOREIGN KEY (ID_Pro) REFERENCES ProDetail (ID),
	CONSTRAINT FK_OrderDetail_OrderPro FOREIGN KEY (ID_Order) REFERENCES OrderPro (ID)
)
CREATE TABLE AdminUser(
    NameUser    NVARCHAR (30) PRIMARY KEY,
	AdminRole	NVARCHAR (MAX) NOT NULL,
    PasswordUser NCHAR (50)     NULL,
)

CREATE TABLE AddressCus(
	ID INT PRIMARY KEY IDENTITY (1, 1),
	PhoneCus INT,
	AddressCus Nvarchar(MAX),
	CONSTRAINT FK_AddressCus_Customer FOREIGN KEY (PhoneCus) REFERENCES Customer(Phone)
);


