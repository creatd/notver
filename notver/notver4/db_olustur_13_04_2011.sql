SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar](
	[HOCA_ID] [int] IDENTITY(1,1) NOT NULL,
	[IS_ACTIVE] [bit] NOT NULL CONSTRAINT [DF_Hocalar_IS_ACTIVE]  DEFAULT ((1)),
	[ISIM] [nvarchar](50) NOT NULL,
	[UNVAN] [nvarchar](50) NULL,
	[YORUM_SAYISI] [int] NULL,
 CONSTRAINT [PK_Hocalar_1] PRIMARY KEY CLUSTERED 
(
	[HOCA_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Okullar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Okullar](
	[OKUL_ID] [int] IDENTITY(1,1) NOT NULL,
	[IS_ACTIVE] [bit] NOT NULL CONSTRAINT [DF_Okullar_IS_ACTIVE]  DEFAULT ((1)),
	[ISIM] [nvarchar](100) NOT NULL,
	[ADRES] [nvarchar](50) NULL,
	[KURULUS_TARIHI] [int] NULL,
	[OGRENCI_SAYISI] [int] NULL,
	[AKADEMIK_SAYISI] [int] NULL,
	[WEB_ADRESI] [nvarchar](256) NULL,
 CONSTRAINT [PK_Okullar_1] PRIMARY KEY CLUSTERED 
(
	[OKUL_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	ID''si verilen dersi siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersSil]
	@DersID int
AS
BEGIN
	DELETE FROM Dersler
	WHERE DERS_ID = @DersID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciIDDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/02/2010
-- Description:	ASP_ID''si verilen kullanicinin UYE tablosundaki UYE_ID''sini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciIDDondur]
	@AspID uniqueidentifier
AS
BEGIN
	SELECT UYE_ID 
	FROM Uyeler
	WHERE ASP_ID = @AspID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaOkulSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 16/02/2011
-- Description:	Hoca okul iliskisini siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaOkulSil]
	@HocaID int,
	@OkulID	int
AS
BEGIN
	DELETE FROM Hocalar_Okullar
	WHERE HOCA_ID = @HocaID AND OKUL_ID = @OkulID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_UyeSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Uyeyi siler (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UyeSil]
	@UyeID int
AS
BEGIN
	DELETE Uyeler
	WHERE UYE_ID = @UyeID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mesajlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Mesajlar](
	[MESAJ_ID] [int] IDENTITY(1,1) NOT NULL,
	[GONDEREN_ID] [int] NOT NULL,
	[ALICI_ID] [int] NOT NULL,
	[BASLIK] [nvarchar](50) NOT NULL,
	[ICERIK] [nvarchar](1024) NOT NULL,
	[GONDERME_ZAMANI] [datetime] NOT NULL,
	[OKUNDU] [bit] NOT NULL CONSTRAINT [DF_Mesajlar_OKUNDU]  DEFAULT ('0'),
 CONSTRAINT [PK_Mesajlar] PRIMARY KEY CLUSTERED 
(
	[MESAJ_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciEpostaAdresiniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 06/03/2011
-- Description:	ID''si verilen kullanicinin kayitli eposta adresini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciEpostaAdresiniDondur]
	@KullaniciID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT EPOSTA
	FROM Uyeler
	WHERE UYE_ID = @KullaniciID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciIsminiDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/03/2011
-- Description:	ID''si verilen kullanicinin ismini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciIsminiDondur]
	@KullaniciEposta nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT AD
	FROM Uyeler
	WHERE EPOSTA = @KullaniciEposta
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciSifreDegistir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/03/2011
-- Description:	Eposta adresi verilen kullanicinin sifresini gunceller
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciSifreDegistir]
	@KullaniciEposta nvarchar(256),
	@YeniSifre nvarchar(128)
AS
BEGIN
    UPDATE Uyeler
	SET SIFRE = @YeniSifre
	WHERE EPOSTA = @KullaniciEposta
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciAdiVarMi]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Uyeler arasinda bu kullanici adi var mi kontrol eder
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciAdiVarMi] 
	@KullaniciAdi nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT COUNT(*) FROM Uyeler
	WHERE LOWER(KULLANICI_ADI) LIKE LOWER(@KullaniciAdi) OR UPPER(KULLANICI_ADI) LIKE UPPER(@KullaniciAdi)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EpostaAdresiVarMi]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Uyeler arasinda bu eposta adresli kullanici var mi kontrol eder
-- =============================================
CREATE PROCEDURE [dbo].[EpostaAdresiVarMi] 
	@Eposta nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT COUNT(*) FROM Uyeler
	WHERE LOWER(EPOSTA) LIKE LOWER(@Eposta) OR UPPER(EPOSTA) LIKE UPPER(@Eposta)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KodaGoreDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Verilen isimdeki dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KodaGoreDersleriDondur]
	@DersKodu nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT d.DERS_ID,
		d.ISIM DERS_ISIM,
		d.KOD,
		d.ACIKLAMA,
		ok.ISIM OKUL_ISIM,
		ok.OKUL_ID
	FROM DERSLER d
	LEFT JOIN OKULLAR ok
	ON ok.OKUL_ID = d.OKUL_ID
	WHERE d.KOD LIKE @DersKodu
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulProfilDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 28/08/2010
-- Description:	Okul profil bilgilerini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkulProfilDondur]
	@OkulID	int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ISIM, 
	ADRES,
	KURULUS_TARIHI,	
	OGRENCI_SAYISI,
	AKADEMIK_SAYISI,
	WEB_ADRESI
	FROM Okullar
	WHERE OKUL_ID = @OkulID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Puan_Aciklama]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Puan_Aciklama](
	[ACIKLAMA] [nvarchar](50) NOT NULL,
	[PUAN_NUMARASI] [int] NOT NULL,
	[IS_ACTIVE] [bit] NOT NULL CONSTRAINT [DF_Hocalar_Puan_Aciklama_IS_ACTIVE]  DEFAULT ((1))
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsmeGoreDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Verilen isimdeki dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[IsmeGoreDersleriDondur]
	@DersIsim nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		d.DERS_ID,
		d.ISIM DERS_ISIM,
		d.KOD,
		d.ACIKLAMA,
		ok.ISIM OKUL_ISIM,
		ok.OKUL_ID
	FROM DERSLER d
	LEFT JOIN OKULLAR ok
	ON ok.OKUL_ID = d.OKUL_ID
	WHERE d.ISIM LIKE @DersIsim
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkullariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Tum aktif okullari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkullariDondur]
AS
BEGIN
	SELECT OKUL_ID , ISIM 
	FROM Okullar 
	WHERE IS_ACTIVE=1
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciOkulYorumunuDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Kullanicinin okul icin yaptigi yorumu dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciOkulYorumunuDondur]
	@OkulID int,
	@KullaniciID int
AS
BEGIN
	SELECT 
	YORUM
	FROM Okullar_Yorumlar
	WHERE KULLANICI_ID = @KullaniciID AND OKUL_ID = @OkulID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciHocayaPuanVermis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/02/2010
-- Description:	Kullanici tarafindan hocaya verilmis aktif bir puan verisi varsa 1, yoksa 0 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciHocayaPuanVermis]
	@KullaniciID int,
	@HocaID int
AS
BEGIN
	SELECT COUNT(*)
	FROM Hocalar_Puanlar hp
	WHERE hp.HOCA_ID = @HocaID AND hp.KULLANICI_ID = @KullaniciID
	AND hp.IS_ACTIVE=1
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Puan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Puan](
	[HOCA_ID] [int] NOT NULL,
	[PUAN1] [float] NULL,
	[PUAN2] [float] NULL,
	[PUAN3] [float] NULL,
	[PUAN4] [float] NULL,
	[PUAN5] [float] NULL,
	[PUAN_SAYISI] [int] NOT NULL CONSTRAINT [DF_Hocalar_Puan_PUAN_SAYISI]  DEFAULT ((0)),
	[YORUM_SAYISI] [int] NOT NULL CONSTRAINT [DF_Hocalar_Puan_YORUM_SAYISI]  DEFAULT ((0))
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dersler_Dosyalar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dersler_Dosyalar](
	[DOSYA_ID] [int] IDENTITY(1,1) NOT NULL,
	[DERS_ID] [int] NOT NULL,
	[HOCA_ID] [int] NULL,
	[DOSYA_KATEGORI_ID] [int] NOT NULL,
	[DOSYA_ISMI] [nvarchar](256) NOT NULL,
	[DOSYA_ADRES] [nvarchar](256) NOT NULL,
	[ACIKLAMA] [nvarchar](256) NULL,
	[EKLENME_TARIHI] [datetime] NOT NULL,
	[EKLEYEN_KULLANICI_ID] [int] NULL,
	[INDIRILME_SAYISI] [int] NOT NULL CONSTRAINT [DF_DERSLER_DOSYALAR_INDIRILME_SAYISI]  DEFAULT ((0)),
	[DOSYA_DURUMU] [int] NULL CONSTRAINT [DF_Dersler_Dosyalar_DOSYA_DURUMU]  DEFAULT ((0)),
	[SILINME_NEDENI] [nvarchar](256) NULL,
	[BOYUT] [int] NULL,
 CONSTRAINT [PK_DERSLER_DOSYALAR] PRIMARY KEY CLUSTERED 
(
	[DOSYA_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Puanlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Puanlar](
	[PUAN_ID] [int] IDENTITY(1,1) NOT NULL,
	[YORUM_DURUMU] [int] NOT NULL CONSTRAINT [DF_Hocalar_Puanlar_IS_ACTIVE_1]  DEFAULT ((0)),
	[HOCA_ID] [int] NOT NULL,
	[KULLANICI_ID] [int] NOT NULL,
	[PUAN1] [int] NOT NULL,
	[PUAN2] [int] NOT NULL,
	[PUAN3] [int] NOT NULL,
	[PUAN4] [int] NOT NULL,
	[PUAN5] [int] NOT NULL,
	[TARIH] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Hocalar_Puanlar_1] PRIMARY KEY CLUSTERED 
(
	[PUAN_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Yorumlar](
	[HOCAYORUM_ID] [int] IDENTITY(1,1) NOT NULL,
	[YORUM_DURUMU] [int] NOT NULL CONSTRAINT [DF_Hocalar_Yorumlar_IS_ACTIVE_1]  DEFAULT ((0)),
	[HOCA_ID] [int] NOT NULL,
	[KULLANICI_ID] [int] NOT NULL,
	[YORUM] [nvarchar](2000) NULL,
	[KULLANICI_PUANARALIGI] [int] NULL,
	[TARIH] [smalldatetime] NOT NULL,
	[ALKIS_PUANI] [int] NOT NULL CONSTRAINT [DF_Hocalar_Yorumlar_ALKIS_PUANI]  DEFAULT ((0)),
	[SILINME_NEDENI] [nvarchar](256) NULL,
 CONSTRAINT [PK_Hocalar_Yorumlar] PRIMARY KEY CLUSTERED 
(
	[HOCAYORUM_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dersler_Yorumlar_Hocalar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dersler_Yorumlar_Hocalar](
	[DERSYORUM_ID] [int] NOT NULL,
	[HOCA_ID] [int] NULL,
	[TAVSIYE_PUANI] [int] NOT NULL,
	[KAYITSIZ_HOCA_ISIM] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Dersler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Dersler](
	[HOCA_ID] [int] NOT NULL,
	[DERS_ID] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Okullar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Okullar](
	[HOCA_ID] [int] NOT NULL,
	[OKUL_ID] [int] NOT NULL,
	[BOLUM_ID] [int] NOT NULL,
	[START_YEAR] [int] NULL,
	[END_YEAR] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar_Dersler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Hocalar_Yorumlar_Dersler](
	[HOCAYORUM_ID] [int] NOT NULL,
	[DERS_ID] [int] NULL,
	[KAYITSIZ_DERS_ISMI] [nvarchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dersler_Yorumlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dersler_Yorumlar](
	[DERSYORUM_ID] [int] IDENTITY(1,1) NOT NULL,
	[YORUM_DURUMU] [int] NOT NULL CONSTRAINT [DF_Dersler_Yorumlar_YORUM_DURUM]  DEFAULT ((0)),
	[DERS_ID] [int] NOT NULL,
	[KULLANICI_ID] [int] NOT NULL,
	[YORUM] [nvarchar](2000) NOT NULL,
	[TARIH] [smalldatetime] NOT NULL,
	[ALKIS_PUANI] [int] NOT NULL,
	[ZORLUK_PUANI] [int] NOT NULL,
	[SILINME_NEDENI] [nvarchar](256) NULL,
 CONSTRAINT [PK_Dersler_Yorumlar] PRIMARY KEY CLUSTERED 
(
	[DERSYORUM_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Okullar_Bolumler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Okullar_Bolumler](
	[BOLUM_ID] [int] IDENTITY(1,1) NOT NULL,
	[OKUL_ID] [int] NOT NULL,
	[ISIM] [nvarchar](256) NOT NULL,
	[IS_ACTIVE] [bit] NULL CONSTRAINT [DF_Okullar_Bolumler_IS_ACTIVE]  DEFAULT ((1)),
 CONSTRAINT [PK_Okullar_Bolumler] PRIMARY KEY CLUSTERED 
(
	[BOLUM_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Uyeler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Uyeler](
	[UYE_ID] [int] IDENTITY(1,1) NOT NULL,
	[EPOSTA] [nvarchar](256) NOT NULL,
	[IS_BLOCKED] [bit] NOT NULL CONSTRAINT [DF_Uyeler_IS_BLOCKED]  DEFAULT ('0'),
	[KULLANICI_ADI] [nvarchar](256) NULL,
	[AD] [nvarchar](50) NOT NULL,
	[SOYAD] [nvarchar](50) NOT NULL,
	[OKUL_ID] [int] NULL,
	[UYELIK_DURUMU] [int] NOT NULL CONSTRAINT [DF_Uyeler_UYELIK_DURUMU]  DEFAULT ((0)),
	[ROL_ID] [int] NOT NULL,
	[SIFRE] [nvarchar](128) NOT NULL,
	[CINSIYET] [bit] NOT NULL,
	[ONAY_PUANI] [int] NULL CONSTRAINT [DF_Uyeler_ONAY_PUANI]  DEFAULT ((0)),
	[BLOK_NEDENI] [nvarchar](256) NULL,
	[BOLUM_ID] [int] NULL,
 CONSTRAINT [PK_Uyeler] PRIMARY KEY CLUSTERED 
(
	[UYE_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Okullar_Yorumlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Okullar_Yorumlar](
	[OKULYORUM_ID] [int] IDENTITY(1,1) NOT NULL,
	[YORUM_DURUMU] [int] NOT NULL CONSTRAINT [DF_Okullar_Yorumlar_YORUM_DURUMU]  DEFAULT ((0)),
	[OKUL_ID] [int] NOT NULL,
	[KULLANICI_ID] [int] NOT NULL,
	[YORUM] [nvarchar](2000) NOT NULL,
	[TARIH] [smalldatetime] NOT NULL,
	[ALKIS_PUANI] [int] NOT NULL CONSTRAINT [DF_Okullar_Yorumlar_ALKIS_PUANI]  DEFAULT ((0)),
	[SILINME_NEDENI] [nvarchar](256) NULL,
 CONSTRAINT [PK_Okullar_Yorumlar] PRIMARY KEY CLUSTERED 
(
	[OKULYORUM_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dersler]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dersler](
	[DERS_ID] [int] IDENTITY(1,1) NOT NULL,
	[OKUL_ID] [int] NOT NULL,
	[IS_ACTIVE] [bit] NULL CONSTRAINT [DF_Dersler_IS_ACTIVE]  DEFAULT ((1)),
	[KOD] [nvarchar](50) NOT NULL,
	[ISIM] [nvarchar](150) NULL,
	[ACIKLAMA] [nvarchar](2000) NULL,
	[BOLUM_ID] [int] NOT NULL,
 CONSTRAINT [PK_Dersler] PRIMARY KEY CLUSTERED 
(
	[DERS_ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Yorumlar_Puanlar]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Yorumlar_Puanlar](
	[YORUM_ID] [int] NOT NULL,
	[KULLANICI_ID] [int] NOT NULL,
	[IS_ACTIVE] [bit] NULL CONSTRAINT [DF_Yorumlar_Puanlar_IS_ACTIVE]  DEFAULT ((1)),
	[OLUMLU_PUAN] [bit] NOT NULL,
	[YORUM_TIPI] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersiVerenHocalariKullaniciyaGoreDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Dersi veren hocalardan, kullanicinin aktif yorumu bulunmayanlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersiVerenHocalariKullaniciyaGoreDondur]
	@DersID int,
	@KullaniciID int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @hocaIDleri TABLE(ID int NULL)
	INSERT INTO @hocaIDleri
	SELECT dyh.HOCA_ID
	FROM Dersler_Yorumlar dy
		RIGHT JOIN Dersler_Yorumlar_Hocalar dyh
		ON dyh.DERSYORUM_ID = dy.DERSYORUM_ID
	WHERE dy.DERS_ID = @DersID AND dy.KULLANICI_ID = @KullaniciID AND (YORUM_DURUMU=1 OR YORUM_DURUMU=0)

	SELECT hd.HOCA_ID,
	ho.ISIM	HOCA_ISIM
	FROM Hocalar_Dersler hd
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = hd.HOCA_ID
	WHERE DERS_ID = @DersID
	AND hd.HOCA_ID NOT IN (SELECT ID FROM @hocaIDleri WHERE ID IS NOT NULL)
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	ID''si verilen hocayi ve hocanin ders-okul iliskilerini siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaSil]
	@HocaID int
AS
BEGIN

	IF NOT EXISTS (SELECT * FROM Hocalar WHERE HOCA_ID = @HocaID)
	BEGIN
		RETURN -1;
	END
	ELSE
	BEGIN
		DELETE FROM Hocalar
		WHERE HOCA_ID = @HocaID

		DELETE FROM Hocalar_Dersler
		WHERE HOCA_ID = @HocaID

		DELETE FROM Hocalar_Okullar	
		WHERE HOCA_ID = @HocaID
	END		
	RETURN 0;
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BolumdekiHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/04/2011
-- Description:	ID''si verilen bolumdeki tum aktif hocalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[BolumdekiHocalariDondur]
	@BolumID int
AS
BEGIN	
	SELECT h.HOCA_ID,
	h.ISIM	HOCA_ISIM,
	h.UNVAN
	FROM Hocalar_Okullar ho	
		LEFT JOIN Hocalar h
		ON h.HOCA_ID = ho.HOCA_ID
	WHERE h.IS_ACTIVE=''True'' AND ho.BOLUM_ID = @BolumID
	ORDER BY h.ISIM

END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Yeni hoca ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaEkle]
	@IsActive bit,
	@Isim nvarchar(50),
	@Unvan nvarchar(50) = NULL,
	@YorumSayisi int = 0,
	@Sonuc int output
AS
BEGIN
	SET NOCOUNT ON;

	IF(EXISTS (SELECT * FROM Hocalar WHERE ISIM = @Isim))	--Cok zayif bir kontrol ama olsun
	BEGIN
		SET @Sonuc = NULL
		RETURN
	END    
	ELSE
	BEGIN
		INSERT INTO Hocalar(IS_ACTIVE,ISIM,UNVAN,YORUM_SAYISI)
		VALUES(@IsActive, @Isim, @Unvan, @YorumSayisi)
		SET @Sonuc = SCOPE_IDENTITY()
	END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumlariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Hoca yorumlarini admin icin dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumlariDondur]
	@HocaID int = NULL,
	@OkulID int = NULL,
	@YorumDurumu int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF (@HocaID IS NOT NULL)
	BEGIN
		SELECT hy.* ,	
			ho.HOCA_ID,
			ho.ISIM HOCA_ISIM
		FROM Hocalar_Yorumlar hy
			LEFT JOIN Hocalar ho
			ON ho.HOCA_ID = hy.HOCA_ID
		WHERE hy.YORUM_DURUMU = COALESCE(@YorumDurumu , hy.YORUM_DURUMU)
			AND hy.HOCA_ID = @HocaID
	END
	ELSE IF (@OkulID IS NOT NULL)
	BEGIN
		SELECT hy.* ,	
			h.HOCA_ID,
			h.ISIM HOCA_ISIM
		FROM Hocalar_Yorumlar hy
			LEFT JOIN Hocalar h
			ON h.HOCA_ID = hy.HOCA_ID
		WHERE hy.YORUM_DURUMU = COALESCE(@YorumDurumu , hy.YORUM_DURUMU)
			AND EXISTS(SELECT * FROM Hocalar_Okullar ho WHERE ho.HOCA_ID=h.HOCA_ID AND ho.OKUL_ID=@OkulID)
	END
	ELSE
	BEGIN
		SELECT hy.* ,	
			ho.HOCA_ID,
			ho.ISIM HOCA_ISIM
		FROM Hocalar_Yorumlar hy
			LEFT JOIN Hocalar ho
			ON ho.HOCA_ID = hy.HOCA_ID
		WHERE hy.YORUM_DURUMU = COALESCE(@YorumDurumu , hy.YORUM_DURUMU)
	END


END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkuldakiHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 04/09/2010
-- Description:	ID''si verilen okuldaki tum hocalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkuldakiHocalariDondur]
	@OkulID int
AS
BEGIN
	DECLARE @HocaIDleri TABLE(ID INT NOT NULL)
	INSERT INTO @HocaIDleri
	SELECT ho.HOCA_ID
	FROM Hocalar_Okullar ho
	WHERE ho.OKUL_ID = @OkulID
	
	SELECT h.HOCA_ID,
	h.ISIM	HOCA_ISIM,
	h.UNVAN
	FROM Hocalar h
	WHERE h.IS_ACTIVE=''True'' AND h.HOCA_ID IN (SELECT ID FROM @HocaIDleri)
	ORDER BY h.ISIM
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersDosyalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Ders dosyalarini dondurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersDosyalariDondur]
	@OkulID int = NULL,
	@DersID int = NULL,	
	@Durum int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF(@DersID IS NOT NULL)
	BEGIN
		SELECT dd.*,
			d.KOD DERS_KOD,	
			d.ISIM DERS_ISIM,
			ok.ISIM OKUL_ISIM,
			ho.HOCA_ID,
			ho.ISIM HOCA_ISIM
		FROM Dersler_Dosyalar dd
			LEFT JOIN Dersler d
			ON d.DERS_ID = dd.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = d.OKUL_ID
			LEFT JOIN Hocalar ho
			ON ho.HOCA_ID = dd.HOCA_ID
		WHERE dd.DOSYA_DURUMU = COALESCE(@Durum , dd.DOSYA_DURUMU)
			AND	dd.DERS_ID = @DersID
	END
	ELSE IF(@OkulID IS NOT NULL)
	BEGIN
		SELECT dd.*,
			d.KOD DERS_KOD,	
			d.ISIM DERS_ISIM,
			ok.ISIM OKUL_ISIM,
			ho.HOCA_ID,
			ho.ISIM HOCA_ISIM
		FROM Dersler_Dosyalar dd
			LEFT JOIN Dersler d
			ON d.DERS_ID = dd.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = d.OKUL_ID
			LEFT JOIN Hocalar ho
			ON ho.HOCA_ID = dd.HOCA_ID
		WHERE dd.DOSYA_DURUMU = COALESCE(@Durum , dd.DOSYA_DURUMU)
			AND	d.OKUL_ID = @OkulID
	END
	ELSE	
	BEGIN
		SELECT dd.*,
			d.KOD DERS_KOD,	
			d.ISIM DERS_ISIM,
			ok.ISIM OKUL_ISIM,
			ho.HOCA_ID,
			ho.ISIM HOCA_ISIM
		FROM Dersler_Dosyalar dd
			LEFT JOIN Dersler d
			ON d.DERS_ID = dd.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = d.OKUL_ID
			LEFT JOIN Hocalar ho
			ON ho.HOCA_ID = dd.HOCA_ID
		WHERE dd.DOSYA_DURUMU = COALESCE(@Durum , dd.DOSYA_DURUMU)
	END	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	ID''si verilen hocayi gunceller
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaGuncelle]
	@HocaID int,
	@IsActive bit,
	@Isim nvarchar(50),
	@Unvan nvarchar(50) = NULL,
	@YorumSayisi int
AS
BEGIN
	UPDATE Hocalar
	SET IS_ACTIVE = @IsActive,
	ISIM = @Isim,
	UNVAN = @Unvan,
	YORUM_SAYISI = @YorumSayisi
	WHERE HOCA_ID = @HocaID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Kayitli butun hocalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocalariDondur]
	@OkulID int = NULL,
	@BolumID int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @BolumID is NULL
	BEGIN
		IF @OkulID is NULL
		BEGIN
			SELECT * FROM Hocalar
		END
		ELSE	
		BEGIN
			SELECT * FROM Hocalar h
			WHERE EXISTS(SELECT * FROM Hocalar_Okullar ho 
						WHERE ho.HOCA_ID = h.HOCA_ID AND ho.OKUL_ID = @OkulID)
		END
	END
	ELSE
	BEGIN
		SELECT * FROM Hocalar h
			WHERE EXISTS(SELECT * FROM Hocalar_Okullar ho 
						WHERE ho.HOCA_ID = h.HOCA_ID AND ho.BOLUM_ID = @BolumID)
	END
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AyniOkuldakiHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/05/2010
-- Description:	Verilen hoca ile ayni okuldan olan hocalar arasindan en populerlerini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[AyniOkuldakiHocalariDondur]
	@HocaID int,
	@Sayi int = 5	--Bu calismiyo
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @okullar table(OkulID int NOT NULL)
	
	INSERT INTO @okullar
	SELECT OKUL_ID 
	FROM Hocalar_Okullar 
	WHERE HOCA_ID = @HocaID 
	
    SELECT DISTINCT TOP 7
	hoc.HOCA_ID,
	hoc.ISIM,
	hp.YORUM_SAYISI
	FROM Hocalar hoc
		LEFT JOIN Hocalar_Okullar ho
		ON ho.HOCA_ID = hoc.HOCA_ID
		LEFT JOIN Hocalar_Puan hp
		ON hp.HOCA_ID = hoc.HOCA_ID
	WHERE ho.OKUL_ID in (SELECT OKUL_ID FROM @okullar)
	AND NOT hoc.HOCA_ID = @HocaID
	ORDER BY hp.YORUM_SAYISI DESC
	
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Hoca yorumu onaylar ve kullanicinin onay puanini yukseltir (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumOnayla]
	@YorumID int,
	@KullaniciID int,
	@OnayliDurumID int,
	@OnayDegeri int
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Hocalar_Yorumlar WHERE HOCAYORUM_ID = @YorumID)
	BEGIN
		RETURN -1;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
	END

	UPDATE Hocalar_Yorumlar
	SET YORUM_DURUMU = @OnayliDurumID
	WHERE HOCAYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI + @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	RETURN 0;

	DECLARE @HocaID int
	SELECT @HocaID = HOCA_ID
	FROM Hocalar_Yorumlar
	WHERE HOCAYORUM_ID = @YorumID

	UPDATE Hocalar
	SET YORUM_SAYISI = YORUM_SAYISI+1
	WHERE HOCA_ID = @HocaID
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciHocaYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/09/2010
-- Description:	Kullanicinin yaptigi tum hoca yorumlarini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciHocaYorumlariniDondur]
	@KullaniciID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT hy.HOCAYORUM_ID,
	hy.YORUM,
	hy.TARIH,
	hy.ALKIS_PUANI,
	hy.YORUM_DURUMU,
	hy.HOCA_ID,
	ho.ISIM HOCA_ISMI
	FROM Hocalar_Yorumlar hy
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = hy.HOCA_ID
	WHERE hy.KULLANICI_ID = @KullaniciID
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciDersYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/09/2010
-- Description:	Kullanicinin yaptigi tum ders yorumlarini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciDersYorumlariniDondur]
	@KullaniciID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dy.DERSYORUM_ID,
	dy.YORUM,
	dy.TARIH,
	dy.ALKIS_PUANI,
	dy.YORUM_DURUMU,
	dy.DERS_ID,
	de.KOD DERS_KODU,
	ok.ISIM OKUL_ISMI,
	ho.ISIM	HOCA_ISIM,
	dyh.KAYITSIZ_HOCA_ISIM
	FROM Dersler_Yorumlar dy
		LEFT JOIN Dersler de
		ON de.DERS_ID = dy.DERS_ID
		LEFT JOIN Okullar ok
		ON ok.OKUL_ID = de.OKUL_ID
		LEFT JOIN Dersler_Yorumlar_Hocalar dyh
		ON dyh.DERSYORUM_ID = dy.DERSYORUM_ID
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = dyh.HOCA_ID
	WHERE dy.KULLANICI_ID = @KullaniciID
	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersDosyalariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/07/2010
-- Description:	Bir dersin, belirtilen kategorisine ait dosyalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersDosyalariniDondur]
	@DersID int,
	@DosyaDurumu int,
	@DosyaKategoriTipi int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dd.DOSYA_ID,
	dd.DOSYA_ISMI,
	dd.DOSYA_ADRES,
	dd.EKLENME_TARIHI,	
	dd.INDIRILME_SAYISI,
	dd.EKLEYEN_KULLANICI_ID,
	dd.ACIKLAMA,
	dd.DOSYA_KATEGORI_ID,
	ho.ISIM HOCA_ISIM
	FROM Dersler_Dosyalar dd
	LEFT JOIN Hocalar ho
	ON ho.HOCA_ID = dd.HOCA_ID
	WHERE DERS_ID = @DersID 
	AND DOSYA_KATEGORI_ID = COALESCE(@DosyaKategoriTipi,DOSYA_KATEGORI_ID)
	AND DOSYA_DURUMU = @DosyaDurumu
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsmeGoreHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Verilen isimdeki hocalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[IsmeGoreHocalariDondur]
	@HocaIsim nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ho.ISIM,
		ho.HOCA_ID,
		ho.UNVAN
	FROM Hocalar ho
	WHERE ho.ISIM LIKE @HocaIsim
	AND IS_ACTIVE=1
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaProfilDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/02/2010
-- Description:	Bir hocanin sayfasinda yer alan profil bilgilerini (hoca ismi, calismis oldugu okullar ve tarihleri) dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaProfilDondur]
	@HocaID int

AS
BEGIN
	SELECT ho.ISIM as HOCA_ISIM,
	ho.UNVAN as HOCA_UNVAN,
	hokul.OKUL_ID ,
	hokul.START_YEAR,
	hokul.END_YEAR,
	okul.ISIM as OKUL_ISIM

	FROM Hocalar ho
	LEFT JOIN Hocalar_Okullar hokul ON hokul.HOCA_ID = @HocaID
	LEFT JOIN Okullar okul ON okul.OKUL_ID = hokul.OKUL_ID
	WHERE ho.HOCA_ID = @HocaID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersiVerenHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Dersi veren tum hocalari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersiVerenHocalariDondur]
	@DersID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT hd.HOCA_ID,
	ho.ISIM	HOCA_ISIM
	FROM Hocalar_Dersler hd
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = hd.HOCA_ID
	WHERE DERS_ID = @DersID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Ders hakkinda yapilan yorumlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersYorumlariniDondur]
	@DersID int
AS
BEGIN
	SELECT 
	dy.DERSYORUM_ID, 
	dy.KULLANICI_ID ,
	uye.KULLANICI_ADI ,
	uye.AD KULLANICI_ISIM,
	dy.YORUM, 
	dy.TARIH ,
	dy.ALKIS_PUANI,
	dy.ZORLUK_PUANI,
	ho.ISIM	HOCA_ISIM,
	dyh.TAVSIYE_PUANI,
	dyh.KAYITSIZ_HOCA_ISIM
	FROM Dersler_Yorumlar dy
		LEFT JOIN Uyeler uye
		ON uye.UYE_ID = dy.KULLANICI_ID	 
		LEFT JOIN Dersler_Yorumlar_Hocalar dyh
		ON dyh.DERSYORUM_ID = dy.DERSYORUM_ID
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = dyh.HOCA_ID
	WHERE dy.YORUM_DURUMU=1 AND dy.DERS_ID = @DersID
	ORDER BY dy.ALKIS_PUANI DESC
END














' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersYorumunuDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 21/11/2010
-- Description:	ID''si verilen ders yorumunu dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersYorumunuDondur]
	@DersYorumID int
AS
BEGIN
	SELECT 	dy.YORUM,
	dy.ZORLUK_PUANI,	
	ho.HOCA_ID,
	ho.ISIM	HOCA_ISIM,
	dyh.KAYITSIZ_HOCA_ISIM,
	dyh.TAVSIYE_PUANI
	FROM Dersler_Yorumlar dy
		LEFT JOIN Dersler_Yorumlar_Hocalar dyh
		ON dyh.DERSYORUM_ID = dy.DERSYORUM_ID
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = dyh.HOCA_ID
	WHERE dy.DERSYORUM_ID = @DersYorumID
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_KayitsizDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/03/2011
-- Description:	Kayitsiz ders isimlerini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_KayitsizDersleriDondur]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT hyd.KAYITSIZ_DERS_ISMI,
	hy.YORUM_DURUMU,
	ho.ISIM	HOCA_ISIM
	FROM Hocalar_Yorumlar_Dersler hyd
		LEFT JOIN Hocalar_Yorumlar hy
		ON hy.HOCAYORUM_ID = hyd.HOCAYORUM_ID
		LEFT JOIN Hocalar ho
		ON ho.HOCA_ID = hy.HOCA_ID
	WHERE KAYITSIZ_DERS_ISMI IS NOT NULL

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SistemTemizle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 23/10/2010
-- Description:	Sistemdeki sonradan eklenen bilgileri siler
-- =============================================
CREATE PROCEDURE [dbo].[SistemTemizle]
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Dersler_Dosyalar
	DELETE FROM Dersler_Yorumlar
	DELETE FROM Dersler_Yorumlar_Hocalar
	DELETE FROM Hocalar_Puan
	DELETE FROM Hocalar_Puanlar
	DELETE FROM Hocalar_Yorumlar
	DELETE FROM Hocalar_Yorumlar_Dersler
	DELETE FROM Mesajlar
	DELETE FROM Okullar_Yorumlar
	DELETE FROM Yorumlar_Puanlar

	UPDATE Hocalar	SET YORUM_SAYISI = 0
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaOkullariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 02/02/2010
-- Description:	Bir hocanin ders vermis bulundugu tum okullari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaOkullariniDondur]
	-- Add the parameters for the stored procedure here
	@HocaID int
AS
BEGIN
	SELECT okul.ISIM,
		 okul.OKUL_ID,
		ho.START_YEAR,
		ho.END_YEAR,
		ob.ISIM BOLUM_ISIM
	FROM Hocalar_Okullar ho
		LEFT JOIN OKULLAR okul on ho.OKUL_ID = okul.OKUL_ID
		LEFT JOIN Okullar_Bolumler ob ON ob.BOLUM_ID = ho.BOLUM_ID
	WHERE ho.HOCA_ID = @HocaID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaOkulEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Hoca ile okul iliskisi ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaOkulEkle]
	@HocaID int,
	@OkulID int,	
	@BaslangicYili int = NULL,	
	@BitisYili int = NULL,
	@BolumID int
AS
BEGIN
	IF (EXISTS (SELECT * FROM Hocalar_Okullar WHERE HOCA_ID = @HocaID AND OKUL_ID = @OkulID))
	BEGIN
		RETURN
	END
	ELSE
	BEGIN
		INSERT INTO Hocalar_Okullar(HOCA_ID , OKUL_ID, START_YEAR, END_YEAR, BOLUM_ID)
		VALUES(@HocaID, @OkulID, @BaslangicYili, @BitisYili, @BolumID)
	END	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Dersi gunceller
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersGuncelle]
	@DersID int,
	@OkulID int,	
	@BolumID int,	
	@IsActive bit,
	@Kod	nvarchar(50),
	@Isim	nvarchar(150) = NULL,
	@Aciklama	nvarchar(2000) = NULL
AS
BEGIN
	UPDATE Dersler
	SET
	OKUL_ID = @OkulID,
	BOLUM_ID = @BolumID,
	IS_ACTIVE = @IsActive,
	KOD = @Kod,
	ISIM = @Isim,
	ACIKLAMA = @Aciklama
	WHERE DERS_ID = @DersID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Kayitli butun dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersleriDondur]
	@OkulID int = NULL,
	@BolumID int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT de.*, ok.ISIM OKUL_ISIM , ob.ISIM BOLUM_ISIM
	FROM Dersler de
		LEFT JOIN Okullar ok ON de.OKUL_ID = ok.OKUL_ID
		LEFT JOIN Okullar_Bolumler ob
		ON ob.OKUL_ID =de.OKUL_ID AND ob.BOLUM_ID = de.BOLUM_ID 
	WHERE de.BOLUM_ID = COALESCE(@BolumID , de.BOLUM_ID) AND
		de.OKUL_ID = COALESCE(@OkulID , de.OKUL_ID) 
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description: Yeni bir ders ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersEkle]
	@IsActive bit,
	@OkulID int,
	@BolumID int,
	@Kod nvarchar(50),
	@Isim nvarchar(100) = NULL,
	@Aciklama nvarchar(2000) = NULL
AS
BEGIN

	IF(EXISTS (SELECT * FROM Dersler WHERE OKUL_ID = @OkulID AND Kod = @Kod))	--Cok zayif bir kontrol ama olsun
	BEGIN
		RETURN
	END    
	ELSE
	BEGIN
		INSERT INTO Dersler(OKUL_ID, BOLUM_ID, IS_ACTIVE, KOD, ISIM, ACIKLAMA)
		VALUES(@OkulID, @BolumID, @IsActive, @Kod, @Isim, @Aciklama)
	END	

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersProfilDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Ders profil bilgilerini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersProfilDondur]
	@DersID	int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	de.OKUL_ID,
	de.KOD,
	de.ISIM,
	de.ACIKLAMA,
	ok.ISIM	OKUL_ISIM,
	ob.ISIM BOLUM_ISIM
	FROM Dersler de
		LEFT JOIN Okullar ok
		ON	ok.OKUL_ID = de.OKUL_ID
		LEFT JOIN Okullar_Bolumler ob
		ON ob.BOLUM_ID = de.BOLUM_ID
	WHERE de.DERS_ID = @DersID
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkuldakiDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	ID''si verilen okuldaki tum dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkuldakiDersleriDondur]
	@OkulID int
AS
BEGIN
	
	SELECT 
	de.DERS_ID,
	de.KOD,
	de.ISIM DERS_ISIM,
	de.ACIKLAMA,
	ok.ISIM	OKUL_ISIM 
	FROM Dersler de
		LEFT JOIN Okullar ok
		ON	ok.OKUL_ID = de.OKUL_ID
	WHERE de.OKUL_ID = @OkulID
	ORDER BY de.KOD
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciYukle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Kullaniciyla ilgili bilgileri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciYukle]
	@Eposta nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	uy.UYE_ID,
	uy.IS_BLOCKED,
	uy.KULLANICI_ADI,
	uy.AD,
	uy.SOYAD,
	uy.OKUL_ID,
	uy.EPOSTA,
	uy.UYELIK_DURUMU,
	uy.ROL_ID,
	uy.CINSIYET,
	uy.ONAY_PUANI,
	ok.ISIM OKUL_ISMI,
	ok.WEB_ADRESI OKUL_URL
	FROM Uyeler uy
	LEFT JOIN Okullar ok
	ON uy.OKUL_ID = ok.OKUL_ID
	WHERE uy.EPOSTA = @Eposta


END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_UyeleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Uyeleri dondurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UyeleriDondur]
	@OkulID int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	--OkulID''yi COALESCE ile verince sorun oluyor (OKUL_ID''si olmayan 
	--kullanicilar donmuyor)
	IF(@OkulID IS NULL)
	BEGIN
		SELECT uy.*,
			ok.ISIM OKUL_ISMI
		FROM Uyeler uy
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = uy.OKUL_ID
	END
	ELSE
	BEGIN
		SELECT uy.*,
			ok.ISIM OKUL_ISMI
		FROM Uyeler uy
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = uy.OKUL_ID
		WHERE ok.OKUL_ID = COALESCE(@OkulID , ok.OKUL_ID)
	END

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_KayitsizHocalariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/03/2011
-- Description:	Kayitsiz hoca isimlerini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_KayitsizHocalariDondur]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dyh.KAYITSIZ_HOCA_ISIM,
	dy.YORUM_DURUMU,
	ok.ISIM KAYITSIZ_HOCA_OKUL
	FROM Dersler_Yorumlar_Hocalar dyh
		LEFT JOIN Dersler_Yorumlar dy
		ON dy.DERSYORUM_ID = dyh.DERSYORUM_ID
		LEFT JOIN Dersler de
		ON de.DERS_ID = dy.DERS_ID
		LEFT JOIN Okullar ok
		ON ok.OKUL_ID = de.OKUL_ID
	WHERE KAYITSIZ_HOCA_ISIM IS NOT NULL

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulUrlDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/03/2011
-- Description:	ID''si verilen okulun url''ni dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkulUrlDondur]
	@OkulID int
AS
	
BEGIN
	SET NOCOUNT ON;

	SELECT WEB_ADRESI
	FROM Okullar
	WHERE OKUL_ID = @OkulID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsmeVeyaKodaGoreDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 21/02/2011
-- Description:	Verilen isimdeki veya koddaki dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[IsmeVeyaKodaGoreDersleriDondur]
	@Anahtar nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT d.DERS_ID,
		d.ISIM DERS_ISIM,
		d.KOD,
		d.ACIKLAMA,
		ok.ISIM OKUL_ISIM,
		ok.OKUL_ID
	FROM DERSLER d
	LEFT JOIN OKULLAR ok
	ON ok.OKUL_ID = d.OKUL_ID
	WHERE d.KOD LIKE @Anahtar
	UNION
	SELECT 
		d.DERS_ID,
		d.ISIM DERS_ISIM,
		d.KOD,
		d.ACIKLAMA,
		ok.ISIM OKUL_ISIM,
		ok.OKUL_ID
	FROM DERSLER d
	LEFT JOIN OKULLAR ok
	ON ok.OKUL_ID = d.OKUL_ID
	WHERE d.ISIM LIKE @Anahtar
	ORDER BY OKUL_ISIM

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersYorumlariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Ders yorumlarini admin icin dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersYorumlariDondur]
	@DersID int = NULL,
	@OkulID int = NULL,
	@YorumDurumu int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF (@DersID IS NOT NULL)
	BEGIN
		SELECT dy.*,
				de.KOD DERS_KOD,
				ok.ISIM OKUL_ISIM
		FROM Dersler_Yorumlar dy
			LEFT JOIN Dersler de
			ON dy.DERS_ID = de.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = de.OKUL_ID
		WHERE dy.DERS_ID = @DersID AND
			dy.YORUM_DURUMU = COALESCE(@YorumDurumu , dy.YORUM_DURUMU)
	END
	ELSE IF (@OkulID IS NOT NULL)
	BEGIN
		SELECT dy.*,
				de.KOD DERS_KOD,
				ok.ISIM OKUL_ISIM
		FROM Dersler_Yorumlar dy
			LEFT JOIN Dersler de
			ON dy.DERS_ID = de.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = de.OKUL_ID
		WHERE de.OKUL_ID = @OkulID AND
			dy.YORUM_DURUMU = COALESCE(@YorumDurumu , dy.YORUM_DURUMU)
	END
	ELSE
	BEGIN
		SELECT dy.*,
				de.KOD DERS_KOD,
				ok.ISIM OKUL_ISIM
		FROM Dersler_Yorumlar dy
			LEFT JOIN Dersler de
			ON dy.DERS_ID = de.DERS_ID
			LEFT JOIN Okullar ok
			ON ok.OKUL_ID = de.OKUL_ID
		WHERE dy.YORUM_DURUMU = COALESCE(@YorumDurumu , dy.YORUM_DURUMU)
	END


	


END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulYorumlariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Okul yorumlarini admin icin dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulYorumlariDondur]
	@OkulID int = NULL,
	@YorumDurumu int = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT oy.*,
		ok.ISIM OKUL_ISIM
	FROM Okullar_Yorumlar oy
		LEFT JOIN	Okullar ok
		ON oy.OKUL_ID = ok.OKUL_ID
	WHERE oy.OKUL_ID = COALESCE(@OkulID , oy.OKUL_ID)
		AND oy.YORUM_DURUMU = COALESCE(@YorumDurumu , oy.YORUM_DURUMU)
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 14/02/2011
-- Description: Yeni bir okul ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulEkle]
	@IsActive bit,
	@Isim nvarchar(100),
	@Adres nvarchar(50) = NULL,
	@KurulusTarihi int = NULL,
	@OgrenciSayisi int = NULL,
	@AkademikSayisi int = NULL,
	@WebAdresi nvarchar(256) = NULL
AS
BEGIN

	IF(EXISTS (SELECT * FROM Okullar WHERE ISIM = @Isim))	--Cok zayif bir kontrol ama olsun
	BEGIN
		RETURN
	END    
	ELSE
	BEGIN
		INSERT INTO Okullar(IS_ACTIVE, ISIM, ADRES, KURULUS_TARIHI, OGRENCI_SAYISI,
			AKADEMIK_SAYISI, WEB_ADRESI)
		VALUES(@IsActive, @Isim, @Adres, @KurulusTarihi, @OgrenciSayisi,
			@AkademikSayisi, @WebAdresi)
	END	

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkullariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Admin paneli icin tum okullari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkullariDondur]
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT * FROM Okullar
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Okul bilgilerini gunceller
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulGuncelle]
	@OkulID int,
	@IsActive bit,
	@Isim nvarchar(100),
	@Adres nvarchar(50) = NULL,
	@KurulusTarihi int = NULL,
	@OgrenciSayisi int = NULL,	
	@AkademikSayisi int = NULL,
	@WebAdresi nvarchar(256) = NULL
AS
BEGIN
	UPDATE Okullar
	SET IS_ACTIVE = @IsActive,
		ISIM = @Isim,
		ADRES = @Adres,	
		KURULUS_TARIHI = @KurulusTarihi,
		OGRENCI_SAYISI = @OgrenciSayisi,
		AKADEMIK_SAYISI = @AkademikSayisi,
		WEB_ADRESI = @WebAdresi
	WHERE OKUL_ID = @OkulID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	ID''si verilen okulu siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulSil]
	@OkulID int
AS
BEGIN
	DELETE FROM Okullar
	WHERE OKUL_ID = @OkulID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulBolumleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/04/2011
-- Description:	Okuldaki tum bolumleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkulBolumleriDondur]
	@OkulID int
AS
BEGIN
	SET NOCOUNT ON;

	IF @OkulID = -99	--Bogazici icin gecici cozum, v3''te duzelt
		SELECT @OkulID = OKUL_ID FROM Okullar WHERE ISIM LIKE ''%BOĞAZİÇİ%''

	SELECT BOLUM_ID, ISIM
	FROM Okullar_Bolumler
	WHERE OKUL_ID = @OkulID AND IS_ACTIVE = ''True''
	ORDER BY ISIM ASC
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BolumdekiDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/04/2011
-- Description: Bir bolumdeki tum aktif dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[BolumdekiDersleriDondur]
	@BolumID int
AS
BEGIN
	SELECT 
	de.DERS_ID,
	de.KOD,
	de.ISIM DERS_ISIM,
	de.ACIKLAMA,
	ok.ISIM	OKUL_ISIM 
	FROM Dersler de
		LEFT JOIN Okullar ok
		ON	ok.OKUL_ID = de.OKUL_ID
	WHERE de.BOLUM_ID = @BolumID	AND de.IS_ACTIVE = ''True''
	ORDER BY de.KOD
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciDerseGenelYorumYapmis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Kullanici tarafindan derse verilmis aktif bir genel yorum varsa 0, yoksa 1 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciDerseGenelYorumYapmis]
	@KullaniciID int,
	@DersID int
AS
BEGIN
	DECLARE @yorumIDleri TABLE(ID int NOT NULL)
	
	INSERT INTO @yorumIDleri
	SELECT DERSYORUM_ID 
	FROM Dersler_Yorumlar dy
	WHERE (dy.YORUM_DURUMU=0 OR dy.YORUM_DURUMU=1) AND dy.DERS_ID = @DersID AND dy.KULLANICI_ID = @KullaniciID

	IF EXISTS (SELECT ID
				FROM @yorumIDleri
				WHERE ID NOT IN (SELECT DERSYORUM_ID FROM Dersler_Yorumlar_Hocalar) )
	BEGIN
		RETURN 0;
	END
	ELSE
	BEGIN
		RETURN 1;
	END
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_KayitsizHocaIliskilendir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/03/2011
-- Description:	Kayitsiz hocayi kayitli hoca ile iliskilendirir
-- =============================================
CREATE PROCEDURE [dbo].[Admin_KayitsizHocaIliskilendir]
	@HocaID int,
	@KayitsizHocaIsim nvarchar(50)
AS
BEGIN
	UPDATE Dersler_Yorumlar_Hocalar
	SET HOCA_ID = @HocaID,
		KAYITSIZ_HOCA_ISIM = NULL
	WHERE KAYITSIZ_HOCA_ISIM = @KayitsizHocaIsim
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersYorumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Ders yorumunu gunceller.
-- Basarili olursa 0 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersYorumGuncelle]
	@DersYorumID int,
	@Yorum nvarchar(2000),
	@ZorlukPuani int,
	@HocaID int	= NULL,
	@TavsiyePuani int = NULL,
	@KayitsizHocaIsim nvarchar(50) = NULL,
	@YorumDurumu int
AS
BEGIN
	UPDATE Dersler_Yorumlar
	SET YORUM = @Yorum,
	ZORLUK_PUANI = @ZorlukPuani,
	TARIH = getdate(),
	YORUM_DURUMU = @YorumDurumu
	WHERE DERSYORUM_ID = @DersYorumID
	
	DELETE FROM Dersler_Yorumlar_Hocalar
	WHERE DERSYORUM_ID = @DersYorumID

	IF (@HocaID IS NOT NULL)
	BEGIN
		INSERT INTO Dersler_Yorumlar_Hocalar(DERSYORUM_ID , HOCA_ID, TAVSIYE_PUANI)
		VALUES(@DersYorumID, @HocaID, @TavsiyePuani)
	END
	ELSE IF(@KayitsizHocaIsim IS NOT NULL)
	BEGIN
		INSERT INTO Dersler_Yorumlar_Hocalar(DERSYORUM_ID , TAVSIYE_PUANI, KAYITSIZ_HOCA_ISIM)
		VALUES(@DersYorumID, @TavsiyePuani, @KayitsizHocaIsim)
	END
	RETURN 0
END














' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersYorumKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Ders icin girilen yorumu kaydeder. Bu ders-hoca kombinasyonu icin kullanicinin daha once yorum yapmadigini varsayar.
-- Basarili olursa 0 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[DersYorumKaydet]
	@DersID int,
	@KullaniciID int,
	@Yorum nvarchar(2000),
	@ZorlukPuani int,
	@HocaID int	= NULL,
	@TavsiyePuani int = NULL,
	@KayitsizHocaIsim nvarchar(50) = NULL,
	@YorumDurumu int
AS
BEGIN
	INSERT INTO Dersler_Yorumlar(YORUM_DURUMU, DERS_ID, KULLANICI_ID, YORUM, TARIH, ALKIS_PUANI, ZORLUK_PUANI)
	VALUES(@YorumDurumu , @DersID, @KullaniciID, @Yorum, getdate() , 0, @ZorlukPuani)
	
	DECLARE @DersYorumID int
	--Demin ekledigimiz yorumun ID''sini aliyoruz
	SELECT @DersYorumID = SCOPE_IDENTITY()
	
	IF (@HocaID IS NOT NULL)
	BEGIN
		INSERT INTO Dersler_Yorumlar_Hocalar(DERSYORUM_ID , HOCA_ID, TAVSIYE_PUANI)
		VALUES(@DersYorumID, @HocaID, @TavsiyePuani)
	END
	ELSE IF(@KayitsizHocaIsim IS NOT NULL)
	BEGIN
		INSERT INTO Dersler_Yorumlar_Hocalar(DERSYORUM_ID , TAVSIYE_PUANI, KAYITSIZ_HOCA_ISIM)
		VALUES(@DersYorumID, @TavsiyePuani, @KayitsizHocaIsim)
	END
	RETURN 0
END













' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaDersEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	Hoca ile ders iliskisi ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaDersEkle]
	@HocaID int,
	@DersID int
AS
BEGIN
	IF (EXISTS (SELECT * FROM Hocalar_Dersler WHERE HOCA_ID = @HocaID AND DERS_ID = @DersID))
	BEGIN
		RETURN
	END
	ELSE
	BEGIN
		INSERT INTO Hocalar_Dersler(HOCA_ID , DERS_ID)
		VALUES(@HocaID, @DersID)
	END	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaDersleriniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 14/02/2010
-- Description:	Bir hocanin vermis oldugu butun dersleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaDersleriniDondur]
	@HocaID int
AS
BEGIN
	SELECT ders.KOD , ders.DERS_ID, ders.ISIM DERS_ISIM,
		okul.ISIM OKUL_ISIM
	FROM Hocalar_Dersler hd
		LEFT JOIN DERSLER ders on hd.DERS_ID = ders.DERS_ID
		LEFT JOIN Okullar okul on okul.OKUL_ID = ders.OKUL_ID
	WHERE hd.HOCA_ID = @HocaID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaDersSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 16/02/2011
-- Description:	Hoca ders iliskisini siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaDersSil]
	@HocaID int,
	@DersID	int
AS
BEGIN
	DELETE FROM Hocalar_Dersler
	WHERE HOCA_ID = @HocaID AND DERS_ID = @DersID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumDersKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/09/2010
-- Description:	ID''si verilen HocaYorum''un ilgili oldugu dersi ID''siyle ya da ismiyle kaydeder
--				Basarili olursa 0, yoksa -1 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumDersKaydet]
	@HocaYorumID int,
	@DersID int = NULL,
	@BilinmeyenDersIsmi	nvarchar(50) = NULL
AS
BEGIN
	IF(@DersID IS NULL)
	BEGIN
		IF(@BilinmeyenDersIsmi IS NULL)
		BEGIN
		RETURN -1
		END
		ELSE
		BEGIN		
			--BilinmeyenDersIsmi''yle ekle
			INSERT INTO Hocalar_Yorumlar_Dersler(HOCAYORUM_ID, KAYITSIZ_DERS_ISMI)
			VALUES(@HocaYorumID , @BilinmeyenDersIsmi)
		END	
	END
	ELSE IF(@BilinmeyenDersIsmi IS NULL)
	BEGIN
		--DersID''siyle ekle
		INSERT INTO Hocalar_Yorumlar_Dersler(HOCAYORUM_ID, DERS_ID)
		VALUES(@HocaYorumID , @DersID)
	END	
	RETURN 0	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumDersleriSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/09/2010
-- Description:	Hoca icin girilen yoruma dair dersleri siler. Basarili olursa 0 dondurur.
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumDersleriSil]
	@HocaYorumID int
AS
BEGIN
	DELETE FROM Hocalar_Yorumlar_Dersler
	WHERE HOCAYORUM_ID = @HocaYorumID

	RETURN 0;
END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumDersleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/09/2010
-- Description:	Verilen HocaYorumID''si ile iliskili DersID veya BilinmeyenDersIsim''leri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumDersleriDondur]
	@HocaYorumID int
AS
BEGIN
	SELECT hyd.DERS_ID,
	hyd.KAYITSIZ_DERS_ISMI,
	de.KOD DERS_KODU,
	ok.ISIM OKUL_ISMI
	FROM Hocalar_Yorumlar_Dersler hyd
		LEFT JOIN Dersler de
		ON hyd.DERS_ID = de.DERS_ID
		LEFT JOIN Okullar ok
		ON de.OKUL_ID = ok.OKUL_ID
	WHERE hyd.HOCAYORUM_ID = @HocaYorumID

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_KayitsizDersIliskilendir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/03/2011
-- Description:	Kayitsiz dersi kayitli ders ile iliskilendirir
-- =============================================
CREATE PROCEDURE [dbo].[Admin_KayitsizDersIliskilendir]
	@DersID int,
	@KayitsizDersIsim nvarchar(50)
AS
BEGIN
	UPDATE Hocalar_Yorumlar_Dersler
	SET DERS_ID = @DersID,
		KAYITSIZ_DERS_ISMI = NULL
	WHERE KAYITSIZ_DERS_ISMI = @KayitsizDersIsim
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 04/02/2010
-- Description:	Hoca hakkinda yapilan yorumlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumlariniDondur]
	@HocaID int
AS
BEGIN
	SELECT DISTINCT
	hy.HOCAYORUM_ID, 
	hy.KULLANICI_ID ,
	uye.KULLANICI_ADI ,
	uye.AD KULLANICI_ISIM,
	hy.YORUM, 
	hy.KULLANICI_PUANARALIGI , 
	hy.TARIH ,
	hy.ALKIS_PUANI,
	hp.PUAN5 as GENEL_PUAN,
	hyd.DERS_ID,
	hyd.KAYITSIZ_DERS_ISMI,
	de.KOD DERS_KODU
	FROM Hocalar_Yorumlar hy
		LEFT JOIN Hocalar_Puanlar hp
		ON hp.KULLANICI_ID = hy.KULLANICI_ID AND hp.HOCA_ID = @HocaID	
		LEFT JOIN Uyeler uye
		ON uye.UYE_ID = hy.KULLANICI_ID	 
		LEFT JOIN Hocalar_Yorumlar_Dersler hyd
		ON hyd.HOCAYORUM_ID = hy.HOCAYORUM_ID
		LEFT JOIN Dersler de
		ON de.DERS_ID = hyd.DERS_ID
	WHERE hy.YORUM_DURUMU=1 AND hy.HOCA_ID = @HocaID AND hp.YORUM_DURUMU=1
	ORDER BY hy.ALKIS_PUANI DESC
END

















' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_IstatistikDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/03/2011
-- Description:	Admin panelindeki istatistikleri dondur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_IstatistikDondur]
	@OnayliYorumDurumID int,
	@OnayliDosyaDurumID int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @OkulYorum int
	DECLARE @OkulYorumOnayli int
	DECLARE @DersYorum int
	DECLARE @DersYorumOnayli int
	DECLARE @HocaYorum int
	DECLARE @HocaYorumOnayli int
	DECLARE @Mesaj int	--Okunmamis mesaj sayisi
	DECLARE @Dosya int
	DECLARE @DosyaOnayli int
	DECLARE @ToplamYorum int
	DECLARE @ToplamYorumOnayli int
	DECLARE @UyeSayisi int
	DECLARE @UyeSayisiToplam int	--Engellenmisler dahil

	SELECT @OkulYorum = COUNT(*)
	FROM Okullar_Yorumlar

	SELECT @OkulYorumOnayli = COUNT(*)
	FROM Okullar_Yorumlar
	WHERE YORUM_DURUMU = @OnayliYorumDurumID

	SELECT @DersYorum = COUNT(*)
	FROM Dersler_Yorumlar

	SELECT @DersYorumOnayli = COUNT(*)
	FROM Dersler_Yorumlar
	WHERE YORUM_DURUMU = @OnayliYorumDurumID

	SELECT @HocaYorum = COUNT(*)
	FROM Hocalar_Yorumlar

	SELECT @HocaYorumOnayli = COUNT(*)
	FROM Hocalar_Yorumlar
	WHERE YORUM_DURUMU = @OnayliYorumDurumID

	SELECT @Mesaj = COUNT(*)
	FROM Mesajlar
	WHERE OKUNDU = ''0''

	SELECT @Dosya = COUNT(*)
	FROM Dersler_Dosyalar

	SELECT @DosyaOnayli = COUNT(*)
	FROM Dersler_Dosyalar
	WHERE DOSYA_DURUMU = @OnayliDosyaDurumID
	
	SELECT @UyeSayisi = COUNT(*)
	FROM Uyeler
	WHERE IS_BLOCKED = ''0''

	SELECT @UyeSayisiToplam = COUNT(*)
	FROM Uyeler
	
	
	
	SET @ToplamYorum = @OkulYorum + @HocaYorum + @DersYorum
	SET @ToplamYorumOnayli = @OkulYorumOnayli + @HocaYorumOnayli + @DersYorumOnayli

	SELECT @OkulYorum OKUL_YORUM,
	 @OkulYorumOnayli OKUL_YORUM_ONAYLI,
	 @DersYorum DERS_YORUM,
	 @DersYorumOnayli DERS_YORUM_ONAYLI,
	 @HocaYorum HOCA_YORUM,
	 @HocaYorumOnayli HOCA_YORUM_ONAYLI,
	 @Mesaj MESAJ_SAYISI,
	 @Dosya DOSYA_SAYISI,
	 @DosyaOnayli DOSYA_SAYISI_ONAYLI,
	 @ToplamYorum TOPLAM_YORUM,
	 @ToplamYorumOnayli	TOPLAM_YORUM_ONAYLI,
	 @UyeSayisi	UYE_SAYISI,
	 @UyeSayisiToplam UYE_SAYISI_TOPLAM
	

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciDerseYorumYapmis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 01/09/2010
-- Description:	Kullanici tarafindan derse verilmis aktif bir yorum varsa 0, yoksa 1 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciDerseYorumYapmis]
	@KullaniciID int,
	@DersID int
AS
BEGIN
	IF EXISTS(SELECT *
	FROM Dersler_Yorumlar dy
	WHERE dy.DERS_ID = @DersID AND dy.KULLANICI_ID = @KullaniciID
	AND (dy.YORUM_DURUMU=0 OR dy.YORUM_DURUMU=1))
	BEGIN
		RETURN 0;
	END
	ELSE
	BEGIN
		RETURN 1;
	END
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersYorumOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Ders yorumu onaylar ve kullanicinin onay puanini yukseltir (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersYorumOnayla]
	@YorumID int,
	@KullaniciID int,
	@OnayliDurumID int,
	@OnayDegeri int
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Dersler_Yorumlar WHERE DERSYORUM_ID = @YorumID)
	BEGIN
		RETURN -1;
		RETURN;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
		RETURN;
	END

	UPDATE Dersler_Yorumlar
	SET YORUM_DURUMU = @OnayliDurumID
	WHERE DERSYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI + @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersYorumYayindanKaldir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Ders yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersYorumYayindanKaldir]
	@YorumID int,
	@KullaniciID int,
	@YeniDurumID int,
	@OnayDegeri int,
	@SilinmeNedeni nvarchar(256) = NULL
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Dersler_Yorumlar WHERE DERSYORUM_ID = @YorumID)
	BEGIN
		RETURN -1;
		RETURN;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
		RETURN;
	END

	UPDATE Dersler_Yorumlar
	SET YORUM_DURUMU = @YeniDurumID,
		SILINME_NEDENI = @SilinmeNedeni
	WHERE DERSYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI - @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YorumPuanVer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR	
-- Create date: 23/02/2010
-- Description:	Bir yoruma verilen olumlu/olumsuz puani kaydeder
--				Daha onceden puan varsa 2, yoksa 1 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[YorumPuanVer] 
	@YorumID int,
	@KullaniciID int,
	@OlumluPuan bit,
	@YorumTipi int,
	@YeniPuan int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @result int	
	IF EXISTS(SELECT * FROM Yorumlar_Puanlar WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi)
	BEGIN
		SET @result = 2
	END
	ELSE
	BEGIN
		SET @result = 1
		INSERT INTO Yorumlar_Puanlar (YORUM_ID , KULLANICI_ID , IS_ACTIVE, OLUMLU_PUAN, YORUM_TIPI)
		VALUES (@YorumID , @KullaniciID, ''True'' , @OlumluPuan, @YorumTipi)
	END
	
--HocaYorum 0
--DersYorum 1
--OkulYorum 2

	DECLARE @puan int
	IF @OlumluPuan = ''True''
		SET @puan = 1
	ELSE	
		SET @puan = -1

	IF @YorumTipi = 0	--HocaYorum
	BEGIN
		IF @result = 1	--Kullanici bu yoruma ilk defa puan veriyor
			UPDATE Hocalar_Yorumlar
			SET ALKIS_PUANI = ALKIS_PUANI + @puan,
			 @YeniPuan = ALKIS_PUANI + @puan
			WHERE HOCAYORUM_ID = @YorumID
		ELSE IF @result = 2		--Kullanici daha once bu yoruma puan vermis
		BEGIN
			DECLARE @EskiPuanOlumlu bit
			SELECT @EskiPuanOlumlu = OLUMLU_PUAN FROM Yorumlar_Puanlar WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			IF NOT ( @EskiPuanOlumlu=@OlumluPuan )
			BEGIN
				UPDATE Hocalar_Yorumlar
				SET ALKIS_PUANI = ALKIS_PUANI + (2 * @puan),
				 @YeniPuan = ALKIS_PUANI + (2 * @puan)
				WHERE HOCAYORUM_ID = @YorumID
				UPDATE Yorumlar_Puanlar
				SET OLUMLU_PUAN = @OlumluPuan
				WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			END
			ELSE
			BEGIN
				SELECT @YeniPuan = ALKIS_PUANI
				FROM Hocalar_Yorumlar
				WHERE HOCAYORUM_ID = @YorumID
			END
		END
	END
	ELSE IF @YorumTipi = 1	--DersYorum
	BEGIN
		IF @result = 1	--Kullanici bu yoruma ilk defa puan veriyor
			UPDATE Dersler_Yorumlar
			SET ALKIS_PUANI = ALKIS_PUANI + @puan,
			 @YeniPuan = ALKIS_PUANI + @puan
			WHERE DERSYORUM_ID = @YorumID
		ELSE IF @result = 2		--Kullanici daha once bu yoruma puan vermis
		BEGIN
			DECLARE @EskiPuanOlumlu1 bit
			SELECT @EskiPuanOlumlu1 = OLUMLU_PUAN FROM Yorumlar_Puanlar WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			IF NOT ( @EskiPuanOlumlu1=@OlumluPuan )
			BEGIN
				UPDATE Dersler_Yorumlar
				SET ALKIS_PUANI = ALKIS_PUANI + (2 * @puan),
				 @YeniPuan = ALKIS_PUANI + (2 * @puan)
				WHERE DERSYORUM_ID = @YorumID
				UPDATE Yorumlar_Puanlar
				SET OLUMLU_PUAN = @OlumluPuan
				WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			END
			ELSE
			BEGIN
				SELECT @YeniPuan = ALKIS_PUANI
				FROM Dersler_Yorumlar
				WHERE DERSYORUM_ID = @YorumID
			END
		END
	END
	ELSE IF @YorumTipi = 2	--OkulYorum
	BEGIN
		IF @result = 1	--Kullanici bu yoruma ilk defa puan veriyor
			UPDATE Hocalar_Yorumlar
			SET ALKIS_PUANI = ALKIS_PUANI + @puan,
			 @YeniPuan = ALKIS_PUANI + @puan
			WHERE HOCAYORUM_ID = @YorumID
		ELSE IF @result = 2		--Kullanici daha once bu yoruma puan vermis
		BEGIN
			DECLARE @EskiPuanOlumlu2 bit
			SELECT @EskiPuanOlumlu2 = OLUMLU_PUAN FROM Yorumlar_Puanlar WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			IF NOT ( @EskiPuanOlumlu2=@OlumluPuan )
			BEGIN
				UPDATE Okullar_Yorumlar
				SET ALKIS_PUANI = ALKIS_PUANI + (2 * @puan),
				 @YeniPuan = ALKIS_PUANI + (2 * @puan)
				WHERE OKULYORUM_ID = @YorumID
				UPDATE Yorumlar_Puanlar
				SET OLUMLU_PUAN = @OlumluPuan
				WHERE YORUM_ID = @YorumID AND KULLANICI_ID  = @KullaniciID AND YORUM_TIPI = @YorumTipi
			END
			ELSE
			BEGIN
				SELECT @YeniPuan = ALKIS_PUANI
				FROM Okullar_Yorumlar
				WHERE OKULYORUM_ID = @YorumID
			END
		END
	END

	RETURN @result
END










' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YorumSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/09/2010
-- Description:	ID''si verilen yorumu siler. Kullanici veya admin tarafindan cagirilabilir. 
-- Basarili olursa 0,
-- Yorumu bulamazsa -1 dondurur.
-- =============================================
CREATE PROCEDURE [dbo].[YorumSil]
	@YorumTipi int,
	@YorumID int,
	@KullaniciIstegiyle bit
AS
BEGIN
	SET NOCOUNT ON;
	
	--HocaYorum = 0,      
    --DersYorum = 1,      
    --OkulYorum = 2
	IF @YorumTipi = 0	--HocaYorum
	BEGIN
		IF NOT EXISTS(SELECT * FROM Hocalar_Yorumlar WHERE HOCAYORUM_ID = @YorumID)
			RETURN -1;

		DECLARE @hocaID int
		DECLARE @kullaniciID int
		SELECT @hocaID = HOCA_ID ,
			@kullaniciID = KULLANICI_ID
		FROM Hocalar_Yorumlar
		WHERE HOCAYORUM_ID = @YorumID AND (YORUM_DURUMU=1 OR YORUM_DURUMU=0)

		DECLARE @eskiPuan1 int
		DECLARE @eskiPuan2 int
		DECLARE @eskiPuan3 int
		DECLARE @eskiPuan4 int
		DECLARE @eskiPuan5 int

		SELECT @eskiPuan1 = PUAN1,
				@eskiPuan2 = PUAN2,
				@eskiPuan3 = PUAN3,
				@eskiPuan4 = PUAN4,
				@eskiPuan5 = PUAN5
		FROM Hocalar_Puanlar
		WHERE HOCA_ID = @hocaID AND KULLANICI_ID = @kullaniciID AND 
		YORUM_DURUMU = 1

		IF(@KullaniciIstegiyle = ''True'')
		BEGIN
			UPDATE Hocalar_Yorumlar
			SET YORUM_DURUMU = 2
			WHERE HOCAYORUM_ID = @YorumID

			UPDATE Hocalar_Puanlar
			SET YORUM_DURUMU = 2
			WHERE HOCA_ID = @hocaID AND KULLANICI_ID = @kullaniciID AND 
			(YORUM_DURUMU = 1 OR YORUM_DURUMU = 0)
		END
		ELSE
		BEGIN
			UPDATE Hocalar_Yorumlar
			SET YORUM_DURUMU = 3
			WHERE HOCAYORUM_ID = @YorumID

			UPDATE Hocalar_Puanlar
			SET YORUM_DURUMU = 3
			WHERE HOCA_ID = @hocaID AND KULLANICI_ID = @kullaniciID AND 
			(YORUM_DURUMU = 1 OR YORUM_DURUMU = 0)
		END
		
		IF(@eskiPuan1 IS NOT NULL)	--Sadece aktif puanlari cektigimiz icin
		--NULL gelmediyse eskiden puanlari aktifmis demektir
		BEGIN
			--Eger puanlari silmeden once aktifseydi, puanlari dusurmeliyiz
			UPDATE Hocalar_Puan	
			SET PUAN1 = PUAN1 - @eskiPuan1,
				PUAN2 = PUAN2 - @eskiPuan2,
				PUAN3 = PUAN3 - @eskiPuan3,
				PUAN4 = PUAN4 - @eskiPuan4,
				PUAN5 = PUAN5 - @eskiPuan5,
				PUAN_SAYISI = PUAN_SAYISI -1,
				YORUM_SAYISI = YORUM_SAYISI-1
			WHERE HOCA_ID = @HocaID
		END

	END
	ELSE IF @YorumTipi = 1	--DersYorum
	BEGIN
		IF NOT EXISTS(SELECT * FROM Dersler_Yorumlar WHERE DERSYORUM_ID = @YorumID)
			RETURN -1;
		IF(@KullaniciIstegiyle = ''True'')
		BEGIN
			UPDATE Dersler_Yorumlar
			SET YORUM_DURUMU = 2
			WHERE DERSYORUM_ID = @YorumID
			AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
		END
		ELSE
		BEGIN
			UPDATE Dersler_Yorumlar
			SET YORUM_DURUMU = 3
			WHERE DERSYORUM_ID = @YorumID
			AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
		END
	END
	ELSE IF @YorumTipi = 2	--OkulYorum
	BEGIN
		IF NOT EXISTS(SELECT * FROM Okullar_Yorumlar WHERE OKULYORUM_ID = @YorumID)
			RETURN -1;
		IF(@KullaniciIstegiyle = ''True'')
		BEGIN
			UPDATE Okullar_Yorumlar
			SET YORUM_DURUMU = 2
			WHERE OKULYORUM_ID = @YorumID
			AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
		END
		ELSE
		BEGIN
			UPDATE Okullar_Yorumlar
			SET YORUM_DURUMU = 3
			WHERE OKULYORUM_ID = @YorumID
			AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
		END
	END

	RETURN 0;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciAktifYorumSayisiniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/09/2010
-- Description:	Kullanicinin aktif yorum sayisini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciAktifYorumSayisiniDondur]
	@KullaniciID int,
	@Sonuc int output
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YorumSayisi1 int;	--Hoca yorumlari
	DECLARE @YorumSayisi2 int;	--Okul yorumlari
	DECLARE @YorumSayisi3 int;	--Ders yorumlari

	SELECT @YorumSayisi1 = COUNT(*)
	FROM Hocalar_Yorumlar
	WHERE KULLANICI_ID = @KullaniciID AND YORUM_DURUMU = 1

	SELECT @YorumSayisi2 = COUNT(*)
	FROM Okullar_Yorumlar
	WHERE KULLANICI_ID = @KullaniciID AND YORUM_DURUMU = 1

	SELECT @YorumSayisi3 = COUNT(*)
	FROM Dersler_Yorumlar
	WHERE KULLANICI_ID = @KullaniciID AND YORUM_DURUMU = 1

	SET @Sonuc = @YorumSayisi1 + @YorumSayisi2 + @YorumSayisi3
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersYorumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Ders yorumunu gunceller (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersYorumGuncelle]
	@DersYorumID int,
	@SilinmeNedeni nvarchar(256) = NULL,
	@DersID int,
	@Yorum nvarchar(2000),
	@GonderilmeTarihi smalldatetime,
	@AlkisPuani int,
	@ZorlukPuani int
AS
BEGIN
	UPDATE Dersler_Yorumlar
	SET SILINME_NEDENI = @SilinmeNedeni,
		DERS_ID = @DersID,
		YORUM = @Yorum,
		TARIH = @GonderilmeTarihi,
		ALKIS_PUANI = @AlkisPuani,
		ZORLUK_PUANI = @ZorlukPuani
	WHERE DERSYORUM_ID = @DersYorumID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersYorumSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Derse ait yorumu siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersYorumSil]
	@DersYorumID int
AS
BEGIN
	DELETE FROM Dersler_Yorumlar
	WHERE DERSYORUM_ID = @DersYorumID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OnayBekleyenYorumlariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Onay bekleyen yorumlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OnayBekleyenYorumlariDondur]
	@YorumTipi int
AS
BEGIN
	SET NOCOUNT ON;

	IF(@YorumTipi = 0)
	BEGIN
		SELECT * FROM Hocalar_Yorumlar
		WHERE YORUM_DURUMU = 0
	END
	ELSE IF(@YorumTipi = 1)
	BEGIN
		SELECT * FROM Dersler_Yorumlar
		WHERE YORUM_DURUMU = 0
	END
	ELSE IF(@YorumTipi = 2)
	BEGIN
		SELECT * FROM Okullar_Yorumlar
		WHERE YORUM_DURUMU = 0
	END


END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciHocaYorumunuDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/02/2010
-- Description:	Kullanicinin hoca icin yaptigi (sadece) aktif yorumu dondurur 
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciHocaYorumunuDondur]
	@HocaID int,
	@KullaniciID int
AS
BEGIN
	SELECT HOCAYORUM_ID,
	YORUM,
	KULLANICI_PUANARALIGI
	FROM Hocalar_Yorumlar
	WHERE KULLANICI_ID = @KullaniciID AND HOCA_ID = @HocaID 
		AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Hoca yorumunu gunceller (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumGuncelle]
	@HocaYorumID int,
	@SilinmeNedeni nvarchar(256) = NULL,
	@HocaID int,
	@KullaniciPuanAraligi int,
	@Yorum nvarchar(2000),
	@GonderilmeTarihi smalldatetime,
	@AlkisPuani int
AS
BEGIN
	UPDATE Hocalar_Yorumlar
	SET SILINME_NEDENI = @SilinmeNedeni,
		HOCA_ID = @HocaID,
		YORUM = @Yorum,
		KULLANICI_PUANARALIGI = @KullaniciPuanAraligi,
		TARIH = @GonderilmeTarihi,
		ALKIS_PUANI = @AlkisPuani
	WHERE HOCAYORUM_ID = @HocaYorumID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumPuanGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/09/2010
-- Description:	Hoca icin girilen yorumu ve puani gunceller. Basarili olursa 0 dondurur.
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumPuanGuncelle]
	@HocaYorumID int,
	@Puan1 int,
	@Puan2 int,
	@Puan3 int,
	@Puan4 int,
	@Puan5 int,
	@Yorum nvarchar(2000),
	@Kullanici_Puanaraligi int,
	@YorumDurumu int
AS
BEGIN
	UPDATE Hocalar_Yorumlar
	SET YORUM = @Yorum,
		KULLANICI_PUANARALIGI = @Kullanici_Puanaraligi,
		TARIH = getdate(),
		YORUM_DURUMU = @YorumDurumu
	WHERE HOCAYORUM_ID = @HocaYorumID

	DECLARE @hocaID int
	DECLARE @kullaniciID int
	SELECT @hocaID = HOCA_ID ,
		@kullaniciID = KULLANICI_ID
	FROM Hocalar_Yorumlar
	WHERE HOCAYORUM_ID = @HocaYorumID
	
	IF (@hocaID IS NOT NULL AND @kullaniciID IS NOT NULL AND @hocaID > 0 
		AND @kullaniciID > 0)
	BEGIN
		DECLARE @eskiPuan1 int
		DECLARE @eskiPuan2 int
		DECLARE @eskiPuan3 int
		DECLARE @eskiPuan4 int
		DECLARE @eskiPuan5 int

		SELECT @eskiPuan1 = PUAN1,
				@eskiPuan2 = PUAN2,
				@eskiPuan3 = PUAN3,
				@eskiPuan4 = PUAN4,
				@eskiPuan5 = PUAN5
		FROM Hocalar_Puanlar
		WHERE HOCA_ID = @hocaID AND KULLANICI_ID = @kullaniciID
			AND YORUM_DURUMU = 1

		IF(@eskiPuan1 IS NULL)
		BEGIN
			SET @eskiPuan1 = 0;
			SET @eskiPuan2 = 0;
			SET @eskiPuan3 = 0;
			SET @eskiPuan4 = 0;
			SET @eskiPuan5 = 0;
		END

		UPDATE Hocalar_Puanlar
		SET PUAN1 = @Puan1,
			PUAN2 = @Puan2,
			PUAN3 = @Puan3,
			PUAN4 = @Puan4,
			PUAN5 = @Puan5,
			TARIH = getdate(),
			YORUM_DURUMU = @YorumDurumu
		WHERE HOCA_ID = @hocaID AND KULLANICI_ID = @kullaniciID

		IF(@YorumDurumu = 1)
		BEGIN
		UPDATE Hocalar_Puan	
			SET PUAN1 = PUAN1 - @eskiPuan1 + @Puan1,
				PUAN2 = PUAN2 - @eskiPuan2 + @Puan2,
				PUAN3 = PUAN3 - @eskiPuan3 + @Puan3,
				PUAN4 = PUAN4 - @eskiPuan4 + @Puan4,
				PUAN5 = PUAN5 - @eskiPuan5 + @Puan5
			WHERE HOCA_ID = @HocaID
		END

		RETURN 0;
	END
	RETURN -1;


END










' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Hocaya ait yorumu siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumSil]
	@HocaYorumID int
AS
BEGIN
	DELETE FROM Hocalar_Yorumlar
	WHERE HOCAYORUM_ID = @HocaYorumID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumYayindanKaldir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Hoca yorumu yayindan kaldirir ve kullanicinin onay puanini dusurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumYayindanKaldir]
	@YorumID int,
	@KullaniciID int,
	@YeniDurumID int,
	@OnayDegeri int,
	@SilinmeNedeni nvarchar(256) = NULL
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Hocalar_Yorumlar WHERE HOCAYORUM_ID = @YorumID)
	BEGIN
		RETURN -1;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
	END

	UPDATE Hocalar_Yorumlar
	SET YORUM_DURUMU = @YeniDurumID,
		SILINME_NEDENI = @SilinmeNedeni
	WHERE HOCAYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI - @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	RETURN 0;
	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaYorumPuanKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/09/2010
-- Description:	Hoca icin girilen yorumu ve puani kaydeder
-- =============================================
CREATE PROCEDURE [dbo].[HocaYorumPuanKaydet]
	@HocaID int,
	@KullaniciID int,
	@puan1 int,
	@puan2 int,
	@puan3 int,
	@puan4 int,
	@puan5 int,
	@Yorum nvarchar(2000),
	@Kullanici_Puanaraligi int,
	@HocaYorumID int output,
	@YorumDurumu int
AS
BEGIN
	INSERT INTO Hocalar_Puanlar(YORUM_DURUMU, HOCA_ID, KULLANICI_ID, PUAN1, PUAN2, PUAN3, PUAN4, PUAN5, TARIH)
	VALUES (@YorumDurumu , @HocaID , @KullaniciID, @puan1, @puan2 , @puan3 , @puan4 , @puan5, getdate())

	INSERT INTO Hocalar_Yorumlar(YORUM_DURUMU, HOCA_ID, KULLANICI_ID, YORUM,
		KULLANICI_PUANARALIGI, TARIH)
	VALUES(@YorumDurumu , @HocaID, @KullaniciID, @Yorum, @Kullanici_puanaraligi ,
	getdate())

	SET @HocaYorumID = SCOPE_IDENTITY()

	--Onay asamasina girecek bir puansa, daha sonra puan onaylandiginda Hocalar_Puan tablosu guncellenecek
	IF(@YorumDurumu = 1)
	BEGIN
		IF EXISTS( SELECT * FROM Hocalar_Puan WHERE HOCA_ID = @HocaID)
		BEGIN
			UPDATE Hocalar_Puan
			SET PUAN1 = PUAN1 + @puan1,
				PUAN2 = PUAN2 + @puan2,
				PUAN3 = PUAN3 + @puan3,
				PUAN4 = PUAN4 + @puan4,
				PUAN5 = PUAN5 + @puan5,
				PUAN_SAYISI = PUAN_SAYISI + 1,
				YORUM_SAYISI = YORUM_SAYISI+1
			WHERE HOCA_ID = @HocaID
		END
		ELSE	
		BEGIN
			INSERT INTO Hocalar_Puan (HOCA_ID, PUAN1 , PUAN2 , PUAN3 , PUAN4 , PUAN5, PUAN_SAYISI, YORUM_SAYISI)
			VALUES(@HocaID , @puan1 , @puan2, @puan3 , @puan4, @puan5 , 1 ,1)
		END
	END

END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_HocaYorumPuanOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 04/03/2011
-- Description:	Hoca yorumu ve puani onaylar ve kullanicinin onay puanini yukseltir (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_HocaYorumPuanOnayla]
	@YorumID int,
	@KullaniciID int,
	@OnayliDurumID int,
	@OnayDegeri int
AS
BEGIN
	DECLARE @HocaID int

	IF NOT EXISTS ( SELECT * FROM Hocalar_Yorumlar WHERE HOCAYORUM_ID = @YorumID)
	BEGIN
		RETURN -1;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
	END
	ELSE
	BEGIN
		SELECT @HocaID = HOCA_ID  FROM Hocalar_Yorumlar WHERE HOCAYORUM_ID = @YorumID
	END

	UPDATE Hocalar_Yorumlar
	SET YORUM_DURUMU = @OnayliDurumID
	WHERE HOCAYORUM_ID = @YorumID	

	DECLARE @Puan1 int
	DECLARE @Puan2 int
	DECLARE @Puan3 int
	DECLARE @Puan4 int
	DECLARE @Puan5 int

	UPDATE Hocalar_Puanlar
	SET YORUM_DURUMU = @OnayliDurumID
	WHERE HOCA_ID = @HocaID AND KULLANICI_ID = @KullaniciID

	SELECT  @Puan1 = PUAN1,
			@Puan2 = PUAN2,
			@Puan3 = PUAN3,
			@Puan4 = PUAN4,
			@Puan5 = PUAN5
	FROM Hocalar_Puanlar
	WHERE HOCA_ID = @HocaID AND KULLANICI_ID = @KullaniciID
	

	--Hocalar_Puan tablosunu guncelle, eger daha once hic bu hocaya puan verilmediyse
	--yeni satir olustur
	IF EXISTS (SELECT * FROM Hocalar_Puan WHERE HOCA_ID = @HocaID)
	BEGIN
		UPDATE Hocalar_Puan
		SET PUAN1 = PUAN1 + @Puan1,
			PUAN2 = PUAN2 + @Puan2,
			PUAN3 = PUAN3 + @Puan3,
			PUAN4 = PUAN4 + @Puan4,
			PUAN5 = PUAN5 + @Puan5,
			PUAN_SAYISI = PUAN_SAYISI + 1,
			YORUM_SAYISI = YORUM_SAYISI + 1
		WHERE HOCA_ID = @HocaID			
	END
	ELSE
	BEGIN
		INSERT INTO Hocalar_Puan(HOCA_ID, PUAN1, PUAN2, PUAN3, PUAN4, PUAN5, PUAN_SAYISI,
			YORUM_SAYISI)
		VALUES(@HocaID, @Puan1, @Puan2, @Puan3, @Puan4, @Puan5, 1,1)
	END

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI + @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	RETURN 0;
	
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciHocayaYorumYapmis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/02/2010
-- Description:	Kullanici tarafindan hocaya verilmis aktif bir yorum varsa 
-- o yorumun ID''sini, yoksa -1 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciHocayaYorumYapmis]
	@KullaniciID int,
	@HocaID int,
	@Sonuc int OUTPUT
AS
BEGIN
	IF EXISTS (SELECT * FROM Hocalar_Yorumlar hy WHERE
			hy.HOCA_ID = @HocaID AND hy.KULLANICI_ID = @KullaniciID
			AND (hy.YORUM_DURUMU=0 OR hy.YORUM_DURUMU=1))
	BEGIN
		SELECT @Sonuc = HOCAYORUM_ID FROM 
			Hocalar_Yorumlar hy WHERE
			hy.HOCA_ID = @HocaID AND hy.KULLANICI_ID = @KullaniciID
			AND (hy.YORUM_DURUMU=0 OR hy.YORUM_DURUMU=1)
	END
	ELSE
	BEGIN	
		SET @Sonuc = -1
	END	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MesajGonder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege
-- Create date: 28/05/2010
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MesajGonder]
	@AliciID int,
	@GonderenID int,
	@Icerik nvarchar(1024),
	@Baslik nvarchar(50),
	@GondermeZamani datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Mesajlar(GONDEREN_ID , ALICI_ID , BASLIK, ICERIK, GONDERME_ZAMANI)
	VALUES(@GonderenID,@AliciID,@Baslik,@Icerik,@GondermeZamani)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MesajYukle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/03/2011
-- Description:	ID''si verilen mesaji yukler
-- =============================================
CREATE PROCEDURE [dbo].[MesajYukle]
	@MesajID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT GONDEREN_ID, ALICI_ID, BASLIK, ICERIK, GONDERME_ZAMANI, OKUNDU
	FROM Mesajlar
	WHERE MESAJ_ID = @MesajID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_MesajOkunduIsaretle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/03/2011
-- Description:	Admine gelen mesaji okundu olarak isaretler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_MesajOkunduIsaretle]
	@MesajID int
AS
BEGIN
	UPDATE 	Mesajlar
	SET OKUNDU = ''1''
	WHERE MESAJ_ID = @MesajID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_MesajSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/03/2011
-- Description:	Admine gelen mesaji sil
-- =============================================
CREATE PROCEDURE [dbo].[Admin_MesajSil]
	@MesajID int
AS
BEGIN
	DELETE 	Mesajlar
	WHERE MESAJ_ID = @MesajID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_MesajlariDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/03/2011
-- Description:	Admine gonderilen mesajlari dondurur. Verilen parametreye gore sadece okunmamislari ya da tumunu dondurur.
-- =============================================
CREATE PROCEDURE [dbo].[Admin_MesajlariDondur]
	@Tumu bit
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT MESAJ_ID, GONDEREN_ID, BASLIK, ICERIK, GONDERME_ZAMANI
	FROM Mesajlar
	WHERE ALICI_ID = -1 AND OKUNDU = ''0'' OR OKUNDU = @Tumu
		
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciHocaPuanlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/02/2010
-- Description:	Kullanicinin hoca icin verdigi (sadece) aktif puanlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciHocaPuanlariniDondur]
	@HocaID int,
	@KullaniciID int
AS
BEGIN
	SELECT 
	PUAN1,
	PUAN2,
	PUAN3,
	PUAN4,
	PUAN5	
	FROM Hocalar_Puanlar
	WHERE KULLANICI_ID = @KullaniciID AND HOCA_ID = @HocaID
		AND (YORUM_DURUMU = 0 OR YORUM_DURUMU = 1)
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulYorumSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Okula ait yorumu siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulYorumSil]
	@OkulYorumID int
AS
BEGIN
	DELETE FROM Okullar_Yorumlar
	WHERE OKULYORUM_ID = @OkulYorumID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulYorumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/20/2011
-- Description:	Okul yorumunu gunceller (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulYorumGuncelle]
	@OkulYorumID int,
	@SilinmeNedeni nvarchar(256) = NULL,
	@OkulID int,
	@Yorum nvarchar(2000),
	@GonderilmeTarihi smalldatetime,
	@AlkisPuani int
AS
BEGIN
	UPDATE Okullar_Yorumlar
	SET SILINME_NEDENI = @SilinmeNedeni,
		OKUL_ID = @OkulID,
		YORUM = @Yorum,
		TARIH = @GonderilmeTarihi,
		ALKIS_PUANI = @AlkisPuani
	WHERE OKULYORUM_ID = @OkulYorumID
	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulYorumOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Okul yorumu onaylar ve kullanicinin onay puanini yukseltir (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulYorumOnayla]
	@YorumID int,
	@KullaniciID int,
	@OnayliDurumID int,
	@OnayDegeri int
AS
BEGIN
	IF NOT EXISTS ( SELECT * FROM Okullar_Yorumlar WHERE OKULYORUM_ID = @YorumID)
	BEGIN		
		RETURN -1;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
	END

	UPDATE Okullar_Yorumlar
	SET YORUM_DURUMU = @OnayliDurumID
	WHERE OKULYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI + @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulYorumYayindanKaldir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Okul yorumunu yayindan kaldirir ve kullanicinin onay puanini dusurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulYorumYayindanKaldir]
	@YorumID int,
	@KullaniciID int,
	@YeniDurumID int,
	@OnayDegeri int,
	@SilinmeNedeni nvarchar(256) = NULL
AS
BEGIN
	IF NOT EXISTS ( SELECT * FROM Okullar_Yorumlar WHERE OKULYORUM_ID = @YorumID)
	BEGIN		
		RETURN -1;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
	END

	UPDATE Okullar_Yorumlar
	SET YORUM_DURUMU = @YeniDurumID,
		SILINME_NEDENI = @SilinmeNedeni
	WHERE OKULYORUM_ID = @YorumID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI - @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciOkulYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/09/2010
-- Description:	Kullanicinin yaptigi tum okul yorumlarini dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciOkulYorumlariniDondur]
	@KullaniciID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT oy.OKULYORUM_ID,
	oy.YORUM,
	oy.TARIH,
	oy.ALKIS_PUANI,
	oy.YORUM_DURUMU,
	oy.OKUL_ID,
	ok.ISIM OKUL_ISMI
	FROM Okullar_Yorumlar oy
		LEFT JOIN Okullar ok
		ON ok.OKUL_ID = oy.OKUL_ID
	WHERE oy.KULLANICI_ID = @KullaniciID
	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciOkulaYorumYapmis]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Kullanici tarafindan okula verilmis aktif bir yorum varsa 1, yoksa 0 dondurur
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciOkulaYorumYapmis]
	@KullaniciID int,
	@OkulID int
AS
BEGIN
	SELECT COUNT(*)
	FROM Okullar_Yorumlar oy
	WHERE oy.OKUL_ID = @OkulID AND oy.KULLANICI_ID = @KullaniciID
	AND (oy.YORUM_DURUMU=1 OR oy.YORUM_DURUMU=0)
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulYorumlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Okul hakkinda yapilan yorumlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkulYorumlariniDondur]
	@OkulID int
AS
BEGIN
	SELECT 
	oy.OKULYORUM_ID, 
	oy.KULLANICI_ID ,
	uye.KULLANICI_ADI ,
	uye.AD KULLANICI_ISIM,
	oy.YORUM, 
	oy.TARIH ,
	oy.ALKIS_PUANI
	FROM Okullar_Yorumlar oy
		LEFT OUTER JOIN Uyeler uye
		ON uye.UYE_ID = oy.KULLANICI_ID	 
	WHERE oy.YORUM_DURUMU=1 AND oy.OKUL_ID = @OkulID
	ORDER BY oy.ALKIS_PUANI DESC
END











' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulYorumKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Okul icin girilen yorumu kaydeder
-- =============================================
CREATE PROCEDURE [dbo].[OkulYorumKaydet]
	@OkulID int,
	@KullaniciID int,
	@Yorum nvarchar(2000),
	@YorumDurumu int
AS
BEGIN
	INSERT INTO Okullar_Yorumlar(YORUM_DURUMU, OKUL_ID, KULLANICI_ID, YORUM, TARIH, ALKIS_PUANI)
	VALUES(@YorumDurumu , @OkulID, @KullaniciID, @Yorum, getdate() , 0)
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulYorumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 29/08/2010
-- Description:	Okul icin girilen yorumu gunceller. Basarili olunca 0 dondurur.
-- =============================================
CREATE PROCEDURE [dbo].[OkulYorumGuncelle]
	@OkulID int,
	@KullaniciID int,
	@Yorum nvarchar(2000),
	@YorumDurumu int
AS
BEGIN
	UPDATE Okullar_Yorumlar
	SET
	YORUM = @Yorum,
	TARIH = getdate(),
	YORUM_DURUMU = @YorumDurumu
	WHERE OKUL_ID = @OkulID AND KULLANICI_ID = @KullaniciID

	RETURN 0;
END









' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Kullanici kaydeder (Kullanici adinin ve eposta adresinin essiz oldugunu varsayar)
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciKaydet]
	@KullaniciAdi nvarchar(256),	--bos gelebilir
	@Ad nvarchar(50),
	@Soyad nvarchar(50),
	@OkulID int =NULL,	--bos gelebilir
	@BolumID int = NULL,
	@Eposta nvarchar(256),
	@UyelikDurumu int,
	@UyelikRol int,	
	@Sifre nvarchar(128),
	@Cinsiyet bit,
	@OnayPuani int
AS
BEGIN
	INSERT INTO Uyeler(IS_BLOCKED,KULLANICI_ADI , AD, SOYAD, OKUL_ID, BOLUM_ID, EPOSTA, UYELIK_DURUMU, ROL_ID, SIFRE, CINSIYET, ONAY_PUANI)
	VALUES(''0'',@KullaniciAdi, @Ad, @Soyad, @OkulID, @BolumID, @Eposta, @UyelikDurumu, @UyelikRol, @Sifre, @Cinsiyet, @OnayPuani)
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciEpostaOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/03/2011
-- Description:	Epostasi verilen kullanicinin epostasini onayli hale getirir
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciEpostaOnayla]
	@KullaniciEposta nvarchar(256),
	@OnayliDurumID int
AS
BEGIN
	UPDATE Uyeler
	SET UYELIK_DURUMU = @OnayliDurumID
	WHERE EPOSTA = @KullaniciEposta
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[KullaniciSifreDogrula]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 07/08/2010
-- Description:	Eposta - sifre kombinasyonunu kontrol eder
-- =============================================
CREATE PROCEDURE [dbo].[KullaniciSifreDogrula]
	@Eposta nvarchar(256),
	@Sifre nvarchar(128)
AS
BEGIN
	DECLARE @UyeID int
	DECLARE @IsBlocked bit
	
	SELECT 	@UyeID = UYE_ID , @IsBlocked = IS_BLOCKED
	FROM Uyeler
	WHERE EPOSTA = @Eposta AND SIFRE = @Sifre

	IF (@UyeID IS NULL OR @UyeID < 0)
	BEGIN
		RETURN -1	-- Eposta-Sifre kombinasyonu mevcut degil
	END	
	ELSE IF(@IsBlocked = ''1'')
	BEGIN
		RETURN -2	-- Kullanici engellenmis durumda
	END
	ELSE
	BEGIN
		RETURN 0	--Sorun yok
	END
	
		
	

		
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_UyeGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 19/02/2011
-- Description:	Uye gunceller (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UyeGuncelle]
	@UyeID int,
	@Eposta nvarchar(256),
	@Bloke int,
	@BlokNedeni nvarchar(256) = NULL,
	@KullaniciAdi nvarchar(256) = NULL,
	@Ad nvarchar(50),
	@Soyad nvarchar(50),
	@OkulID int = NULL,
	@UyelikDurumu int,
	@UyelikRol int,
	@KizMi bit,
	@OnayPuani int
AS
BEGIN
	UPDATE Uyeler
	SET	EPOSTA = @Eposta,
		IS_BLOCKED = @Bloke,
		BLOK_NEDENI = @BlokNedeni,
		KULLANICI_ADI = @KullaniciAdi,
		AD = @Ad,
		SOYAD=  @Soyad,
		OKUL_ID = @OkulID,
		UYELIK_DURUMU = @UyelikDurumu,
		ROL_ID = @UyelikRol,
		CINSIYET = @KizMi,
		ONAY_PUANI = @OnayPuani
	WHERE UYE_ID = @UyeID
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaPuanlariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 04/02/2010
-- Description:	Bir hocaya verilmis puanlari dondurur
-- =============================================
CREATE PROCEDURE [dbo].[HocaPuanlariniDondur]
	-- Add the parameters for the stored procedure here
	@HocaID int
AS
BEGIN
	SELECT PUAN1, PUAN2, PUAN3, PUAN4, PUAN5, PUAN_SAYISI
	FROM Hocalar_Puan
	WHERE HOCA_ID = @HocaID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HocaPuanAciklamalariniDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HocaPuanAciklamalariniDondur]
AS
BEGIN
	SELECT ACIKLAMA , PUAN_NUMARASI
	from Hocalar_Puan_Aciklama hpa
	WHERE hpa.IS_ACTIVE = 1
	ORDER BY PUAN_NUMARASI
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersDosyaIsmiVarMi]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 12/02/2011
-- Description:	Ayni ders dosya ismi var mi kontrol et (Amazon''da cakisma olmasin diye)
-- =============================================
CREATE PROCEDURE [dbo].[DersDosyaIsmiVarMi]
	@DersID int,
	@DosyaKategoriTipi int,
	@DosyaIsmi nvarchar(256) -- orn. 2.pdf
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM Dersler_Dosyalar WHERE DERS_ID = @DersID AND DOSYA_KATEGORI_ID = @DosyaKategoriTipi AND DOSYA_ISMI = @DosyaIsmi)
	BEGIN
		RETURN 1;	-- Sorun var, boyle bir isim var
	END
	ELSE
	BEGIN
		RETURN 0;	-- Sorun yok
	END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersDosyasiniKaydet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 08/08/2010
-- Description:	Ders dosyasini kaydeder
-- =============================================
CREATE PROCEDURE [dbo].[DersDosyasiniKaydet]
	@DersID int,
	@HocaID int = NULL,
	@DosyaKategoriTipi int,
	@DosyaIsmi nvarchar(256),
	@DosyaAdres nvarchar(256),
	@YukleyenKullaniciID int,	
	@Aciklama nvarchar(256) = NULL,
	@YuklemeTarihi datetime,
	@Boyut int,
	@DosyaDurumu int
	
AS
BEGIN
	INSERT INTO Dersler_Dosyalar(DERS_ID, HOCA_ID, DOSYA_KATEGORI_ID, 
DOSYA_ISMI, DOSYA_ADRES, ACIKLAMA, EKLENME_TARIHI, EKLEYEN_KULLANICI_ID,
BOYUT,DOSYA_DURUMU)
	VALUES(@DersID, @HocaID, @DosyaKategoriTipi, @DosyaIsmi, @DosyaAdres, 
@Aciklama, @YuklemeTarihi, @YukleyenKullaniciID, @Boyut, @DosyaDurumu)
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DersDosyaIndirildi]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/04/2011
-- Description:	Dosya indirildi, dosyanin indirilme sayisini bir artirir
-- =============================================
CREATE PROCEDURE [dbo].[DersDosyaIndirildi]
	@DosyaID int
AS
BEGIN
	UPDATE Dersler_Dosyalar
	SET INDIRILME_SAYISI = INDIRILME_SAYISI + 1
	WHERE DOSYA_ID = @DosyaID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersDosyaOnayla]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 06/03/2011
-- Description:	Dosyayi onaylar ve kullanicinin onay puanini yukseltir (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersDosyaOnayla]
	@DosyaID int,
	@KullaniciID int,
	@OnayliDurumID int,
	@OnayDegeri int
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Dersler_Dosyalar WHERE DOSYA_ID = @DosyaID)
	BEGIN
		RETURN -1;
		RETURN;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
		RETURN;
	END

	UPDATE Dersler_Dosyalar
	SET DOSYA_DURUMU = @OnayliDurumID
	WHERE DOSYA_ID = @DosyaID

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI + @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersDosyaYayindanKaldir]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 06/03/2011
-- Description:	Ders dosyayi yayindan kaldirir ve kullanicinin onay puanini dusurur (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersDosyaYayindanKaldir]
	@DosyaID int,
	@KullaniciID int,
	@YeniDurumID int,
	@OnayDegeri int,
	@SilinmeNedeni nvarchar(256) = NULL
AS
BEGIN
	
	IF NOT EXISTS ( SELECT * FROM Dersler_Dosyalar WHERE DOSYA_ID = @DosyaID)
	BEGIN
		RETURN -1;
		RETURN;
	END
	ELSE IF NOT EXISTS ( SELECT * FROM Uyeler WHERE UYE_ID = @KullaniciID)
	BEGIN
		RETURN -2;
		RETURN;
	END

	UPDATE Dersler_Dosyalar
	SET DOSYA_DURUMU = @YeniDurumID,
		SILINME_NEDENI = @SilinmeNedeni
	WHERE DOSYA_ID = @DosyaID	

	UPDATE Uyeler
	SET ONAY_PUANI = ONAY_PUANI - @OnayDegeri
	WHERE UYE_ID = @KullaniciID
	
	RETURN 0;
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersDosyaGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 18/02/2011
-- Description:	Ders dosyasini gunceller (Admin)
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersDosyaGuncelle]
	@DosyaID int,
	@DersID int,
	@HocaID int = NULL,
	@DosyaKategori int,
	@SilinmeNedeni nvarchar(256) = NULL,
	@DosyaIsmi nvarchar(256),
	@DosyaAdresi nvarchar(256),
	@Aciklama nvarchar(256),
	@EklenmeTarihi smalldatetime,
	@EkleyenKullanici int,
	@IndirilmeSayisi int
AS
BEGIN
	UPDATE Dersler_Dosyalar
	SET
		DERS_ID = @DersID,
		HOCA_ID = @HocaID,
		DOSYA_KATEGORI_ID = @DosyaKategori,
		DOSYA_ISMI = @DosyaIsmi,
		DOSYA_ADRES = @DosyaAdresi,
		ACIKLAMA = @Aciklama,
		EKLENME_TARIHI = @EklenmeTarihi,
		EKLEYEN_KULLANICI_ID = @EkleyenKullanici,
		INDIRILME_SAYISI = @IndirilmeSayisi,
		SILINME_NEDENI = @SilinmeNedeni
	WHERE DOSYA_ID = @DosyaID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_DersDosyaSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 15/02/2011
-- Description:	ID''si verilen ders dosyasini siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DersDosyaSil]
	@DosyaID int
AS
BEGIN
	DELETE FROM Dersler_Dosyalar
	WHERE DOSYA_ID = @DosyaID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulBolumleriDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/04/2011
-- Description:	Admin paneli icin bir okuldaki tum bolumleri dondurur
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulBolumleriDondur]
	@OkulID int
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT * FROM Okullar_Bolumler
	WHERE OKUL_ID = @OkulID
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_BolumSil]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/04/2011
-- Description:	ID''si verilen bolumu siler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_BolumSil]
	@BolumID int
AS
BEGIN
	DELETE FROM Okullar_Bolumler
	WHERE BOLUM_ID = @BolumID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_BolumGuncelle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 11/04/2011
-- Description:	Admin - bolumu gunceller
-- =============================================
CREATE PROCEDURE [dbo].[Admin_BolumGuncelle]
	@BolumID int,
	@BolumIsim nvarchar(256),
	@IsActive bit
AS
BEGIN
	UPDATE Okullar_Bolumler
	SET ISIM = @BolumIsim,
	IS_ACTIVE = @IsActive
	WHERE BOLUM_ID = @BolumID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin_OkulBolumEkle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 06/04/2011
-- Description:	Bilgileri verilen bolumu ekler
-- =============================================
CREATE PROCEDURE [dbo].[Admin_OkulBolumEkle]
	@OkulID int,
	@BolumIsim nvarchar(256),
	@IsActive bit
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Okullar_Bolumler
					WHERE OKUL_ID = @OkulID AND ISIM = @BolumIsim)
	BEGIN	
		INSERT INTO Okullar_Bolumler(OKUL_ID , ISIM, IS_ACTIVE)
		VALUES(@OkulID, @BolumIsim, @IsActive)
	END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OkulBolumDondur]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Ege AKPINAR
-- Create date: 05/04/2011
-- Description:	ID''si verilen bolumu dondurur
-- =============================================
CREATE PROCEDURE [dbo].[OkulBolumDondur]
	@BolumID int
AS
BEGIN
	SELECT ISIM , BOLUM_ID
	FROM Okullar_Bolumler
	WHERE BOLUM_ID = @BolumID AND IS_ACTIVE = ''True''
END
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Puan_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Puan]'))
ALTER TABLE [dbo].[Hocalar_Puan]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Puan_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Hocalar_Puan] CHECK CONSTRAINT [FK_Hocalar_Puan_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Dosyalar_Dersler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Dosyalar]'))
ALTER TABLE [dbo].[Dersler_Dosyalar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Dosyalar_Dersler] FOREIGN KEY([DERS_ID])
REFERENCES [dbo].[Dersler] ([DERS_ID])
GO
ALTER TABLE [dbo].[Dersler_Dosyalar] CHECK CONSTRAINT [FK_Dersler_Dosyalar_Dersler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Dosyalar_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Dosyalar]'))
ALTER TABLE [dbo].[Dersler_Dosyalar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Dosyalar_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Dersler_Dosyalar] CHECK CONSTRAINT [FK_Dersler_Dosyalar_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Dosyalar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Dosyalar]'))
ALTER TABLE [dbo].[Dersler_Dosyalar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Dosyalar_Uyeler] FOREIGN KEY([EKLEYEN_KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Dersler_Dosyalar] CHECK CONSTRAINT [FK_Dersler_Dosyalar_Uyeler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Puanlar_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Puanlar]'))
ALTER TABLE [dbo].[Hocalar_Puanlar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Puanlar_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Hocalar_Puanlar] CHECK CONSTRAINT [FK_Hocalar_Puanlar_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Puanlar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Puanlar]'))
ALTER TABLE [dbo].[Hocalar_Puanlar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Puanlar_Uyeler] FOREIGN KEY([KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Hocalar_Puanlar] CHECK CONSTRAINT [FK_Hocalar_Puanlar_Uyeler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Yorumlar_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar]'))
ALTER TABLE [dbo].[Hocalar_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Yorumlar_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Hocalar_Yorumlar] CHECK CONSTRAINT [FK_Hocalar_Yorumlar_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Yorumlar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar]'))
ALTER TABLE [dbo].[Hocalar_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Yorumlar_Uyeler] FOREIGN KEY([KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Hocalar_Yorumlar] CHECK CONSTRAINT [FK_Hocalar_Yorumlar_Uyeler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Yorumlar_Hocalar_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Yorumlar_Hocalar]'))
ALTER TABLE [dbo].[Dersler_Yorumlar_Hocalar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Yorumlar_Hocalar_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Dersler_Yorumlar_Hocalar] CHECK CONSTRAINT [FK_Dersler_Yorumlar_Hocalar_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Dersler_Dersler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Dersler]'))
ALTER TABLE [dbo].[Hocalar_Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Dersler_Dersler] FOREIGN KEY([DERS_ID])
REFERENCES [dbo].[Dersler] ([DERS_ID])
GO
ALTER TABLE [dbo].[Hocalar_Dersler] CHECK CONSTRAINT [FK_Hocalar_Dersler_Dersler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Dersler_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Dersler]'))
ALTER TABLE [dbo].[Hocalar_Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Dersler_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Hocalar_Dersler] CHECK CONSTRAINT [FK_Hocalar_Dersler_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Okullar_Hocalar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Okullar]'))
ALTER TABLE [dbo].[Hocalar_Okullar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Okullar_Hocalar] FOREIGN KEY([HOCA_ID])
REFERENCES [dbo].[Hocalar] ([HOCA_ID])
GO
ALTER TABLE [dbo].[Hocalar_Okullar] CHECK CONSTRAINT [FK_Hocalar_Okullar_Hocalar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Okullar_Okullar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Okullar]'))
ALTER TABLE [dbo].[Hocalar_Okullar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Okullar_Okullar] FOREIGN KEY([OKUL_ID])
REFERENCES [dbo].[Okullar] ([OKUL_ID])
GO
ALTER TABLE [dbo].[Hocalar_Okullar] CHECK CONSTRAINT [FK_Hocalar_Okullar_Okullar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Okullar_Okullar_Bolumler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Okullar]'))
ALTER TABLE [dbo].[Hocalar_Okullar]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Okullar_Okullar_Bolumler] FOREIGN KEY([BOLUM_ID])
REFERENCES [dbo].[Okullar_Bolumler] ([BOLUM_ID])
GO
ALTER TABLE [dbo].[Hocalar_Okullar] CHECK CONSTRAINT [FK_Hocalar_Okullar_Okullar_Bolumler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Yorumlar_Dersler_Dersler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar_Dersler]'))
ALTER TABLE [dbo].[Hocalar_Yorumlar_Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Yorumlar_Dersler_Dersler] FOREIGN KEY([DERS_ID])
REFERENCES [dbo].[Dersler] ([DERS_ID])
GO
ALTER TABLE [dbo].[Hocalar_Yorumlar_Dersler] CHECK CONSTRAINT [FK_Hocalar_Yorumlar_Dersler_Dersler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Hocalar_Yorumlar_Dersler_Hocalar_Yorumlar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Hocalar_Yorumlar_Dersler]'))
ALTER TABLE [dbo].[Hocalar_Yorumlar_Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Hocalar_Yorumlar_Dersler_Hocalar_Yorumlar] FOREIGN KEY([HOCAYORUM_ID])
REFERENCES [dbo].[Hocalar_Yorumlar] ([HOCAYORUM_ID])
GO
ALTER TABLE [dbo].[Hocalar_Yorumlar_Dersler] CHECK CONSTRAINT [FK_Hocalar_Yorumlar_Dersler_Hocalar_Yorumlar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Yorumlar_Dersler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Yorumlar]'))
ALTER TABLE [dbo].[Dersler_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Yorumlar_Dersler] FOREIGN KEY([DERS_ID])
REFERENCES [dbo].[Dersler] ([DERS_ID])
GO
ALTER TABLE [dbo].[Dersler_Yorumlar] CHECK CONSTRAINT [FK_Dersler_Yorumlar_Dersler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Yorumlar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler_Yorumlar]'))
ALTER TABLE [dbo].[Dersler_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Yorumlar_Uyeler] FOREIGN KEY([KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Dersler_Yorumlar] CHECK CONSTRAINT [FK_Dersler_Yorumlar_Uyeler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Okullar_Bolumler_Okullar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Okullar_Bolumler]'))
ALTER TABLE [dbo].[Okullar_Bolumler]  WITH CHECK ADD  CONSTRAINT [FK_Okullar_Bolumler_Okullar] FOREIGN KEY([OKUL_ID])
REFERENCES [dbo].[Okullar] ([OKUL_ID])
GO
ALTER TABLE [dbo].[Okullar_Bolumler] CHECK CONSTRAINT [FK_Okullar_Bolumler_Okullar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Okullar_Bolumler_Okullar_Bolumler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Okullar_Bolumler]'))
ALTER TABLE [dbo].[Okullar_Bolumler]  WITH CHECK ADD  CONSTRAINT [FK_Okullar_Bolumler_Okullar_Bolumler] FOREIGN KEY([BOLUM_ID])
REFERENCES [dbo].[Okullar_Bolumler] ([BOLUM_ID])
GO
ALTER TABLE [dbo].[Okullar_Bolumler] CHECK CONSTRAINT [FK_Okullar_Bolumler_Okullar_Bolumler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Uyeler_Okullar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Uyeler]'))
ALTER TABLE [dbo].[Uyeler]  WITH CHECK ADD  CONSTRAINT [FK_Uyeler_Okullar] FOREIGN KEY([OKUL_ID])
REFERENCES [dbo].[Okullar] ([OKUL_ID])
GO
ALTER TABLE [dbo].[Uyeler] CHECK CONSTRAINT [FK_Uyeler_Okullar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Okullar_Yorumlar_Okullar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Okullar_Yorumlar]'))
ALTER TABLE [dbo].[Okullar_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Okullar_Yorumlar_Okullar] FOREIGN KEY([OKUL_ID])
REFERENCES [dbo].[Okullar] ([OKUL_ID])
GO
ALTER TABLE [dbo].[Okullar_Yorumlar] CHECK CONSTRAINT [FK_Okullar_Yorumlar_Okullar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Okullar_Yorumlar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Okullar_Yorumlar]'))
ALTER TABLE [dbo].[Okullar_Yorumlar]  WITH CHECK ADD  CONSTRAINT [FK_Okullar_Yorumlar_Uyeler] FOREIGN KEY([KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Okullar_Yorumlar] CHECK CONSTRAINT [FK_Okullar_Yorumlar_Uyeler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Okullar]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler]'))
ALTER TABLE [dbo].[Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Okullar] FOREIGN KEY([OKUL_ID])
REFERENCES [dbo].[Okullar] ([OKUL_ID])
GO
ALTER TABLE [dbo].[Dersler] CHECK CONSTRAINT [FK_Dersler_Okullar]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Dersler_Okullar_Bolumler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dersler]'))
ALTER TABLE [dbo].[Dersler]  WITH CHECK ADD  CONSTRAINT [FK_Dersler_Okullar_Bolumler] FOREIGN KEY([BOLUM_ID])
REFERENCES [dbo].[Okullar_Bolumler] ([BOLUM_ID])
GO
ALTER TABLE [dbo].[Dersler] CHECK CONSTRAINT [FK_Dersler_Okullar_Bolumler]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Yorumlar_Puanlar_Uyeler]') AND parent_object_id = OBJECT_ID(N'[dbo].[Yorumlar_Puanlar]'))
ALTER TABLE [dbo].[Yorumlar_Puanlar]  WITH CHECK ADD  CONSTRAINT [FK_Yorumlar_Puanlar_Uyeler] FOREIGN KEY([KULLANICI_ID])
REFERENCES [dbo].[Uyeler] ([UYE_ID])
GO
ALTER TABLE [dbo].[Yorumlar_Puanlar] CHECK CONSTRAINT [FK_Yorumlar_Puanlar_Uyeler]
