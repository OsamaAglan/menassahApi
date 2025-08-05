

/****** Object:  StoredProcedure [dbo].[spx_Set_Inv_Reported]    Script Date: 16/04/2025 05:59:17 „ ******/
DROP PROCEDURE [dbo].[spx_Set_Inv_Reported]
GO

/****** Object:  StoredProcedure [dbo].[spx_Invs_Not_Reported]    Script Date: 16/04/2025 05:59:17 „ ******/
DROP PROCEDURE [dbo].[spx_Invs_Not_Reported]
GO

/****** Object:  StoredProcedure [dbo].[spx_Invs_Not_Reported]    Script Date: 16/04/2025 05:59:17 „ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spx_Invs_Not_Reported]
AS
BEGIN

    -- ÃœÊ· „ƒﬁ  ÌÕ ÊÌ ⁄·Ï √—ﬁ«„ «·›Ê« Ì— «·„ÿ·Ê»… ›ﬁÿ
    DECLARE @TargetInvs TABLE (fInvID INT)

    INSERT INTO @TargetInvs (fInvID)
    select TOP 10   fInvID from  tblSalesOrderHdr  
	where fTransed = 1	
	AND Reported = 0 
		order by finvid desc

    -- «·«” ⁄·«„ «·—∆Ì”Ì
    SELECT     
        soh.fInvID AS 'invId', 
        CAST(soh.fDate AS date) AS 'invDate', 
        tc.fName AS 'custName', 
        tc.fNameA AS 'custNameA', 
        soh.fDescription AS 'description', 
        dtlData.total AS 'invTotal', 
        soh.fDiscVal AS 'discValHdr', 
        dtlData.dtlDiscVal AS 'discValDtl',
        soh.fDiscVal + dtlData.dtlDiscVal AS 'totalDiscVal',
        soh.fTaxVal AS 'taxValHdr', 
        dtlData.dtlTaxVal AS 'taxValDtl',
        soh.fTaxVal + dtlData.dtlTaxVal AS 'totalTaxVal', 
        soh.fInvValue AS 'invValue', 
        tcr.fCurNameA AS 'curName',
        soh.Rate AS 'rate',
        soh.fLocalValue AS 'localValue',
        soh.fDiscount AS 'discount', 
        soh.fTax AS 'tax', 
        tc.fAddress AS 'arabicAddress', 
        tc.English_Address AS 'englishAddress', 
        tc.fZipCode AS 'zipCode', 
        tc.fTelephone AS 'fTelephone', 
        tc.fMobile AS 'fMobile', 
        tc.fTel2 AS 'fTel2', 
        tc.Vat_ID_No AS 'Vat_ID_No', 
        tc.National_Number AS 'National_Number', 
        tc.Commercial_Register AS 'commercialRegister', 
        tblSlsCountry.fName AS 'countryName', 
        tblSlsCountry.fNameA AS 'countryNameA',
        tblSlsCity.fName AS 'cityName', 
        tblSlsCity.fNameA AS 'cityNameA', 
        tblSlsDist.fName AS 'regionName', 
        tblSlsDist.fNameA AS 'regionNameA', 
        tblSlsArea.fName AS 'areaName', 
        tblSlsArea.fNameA AS 'areaNameA'

    FROM tblSlsCity 
    RIGHT OUTER JOIN tblSlsDist 
        RIGHT OUTER JOIN tblSalesOrderHdr AS soh 
            INNER JOIN tblCustomers AS tc ON tc.fID = soh.fCustomerID 
            INNER JOIN tblCurrency AS tcr ON tcr.fCurCode = soh.Currency 
        ON tblSlsDist.fID = tc.fDistID 
        LEFT OUTER JOIN tblSlsCountry 
            INNER JOIN tblSlsArea ON tblSlsCountry.fID = tblSlsArea.fCountryID 
        ON tc.fAreaID = tblSlsArea.fID 
    ON tblSlsCity.fID = tc.fCityID

    LEFT OUTER JOIN (
        SELECT 
            fInvID, 
            SUM(fPrice * fQuantity) AS total,
            SUM(fItemTaxValue) AS dtlTaxVal, 
            SUM(fDiscVal) AS dtlDiscVal
        FROM tblSalesOrderDTLGL 
        GROUP BY fInvID
    ) AS dtlData ON dtlData.fInvID = soh.fInvID

    WHERE   soh.fInvID IN (SELECT fInvID FROM @TargetInvs)


    -- «” ⁄·«„ «· ›«’Ì· (Ì„ﬂ‰ﬂ  Œ’Ì’Â √Ì÷« »‰›” «·›Ê« Ì—)
    SELECT 
        fInvID, 
        fPrice,
        fQuantity,
        fItemTaxValue, 
        fDiscVal
    FROM tblSalesOrderDTLGL 
    WHERE fInvID IN (SELECT fInvID FROM @TargetInvs)

END
GO

/****** Object:  StoredProcedure [dbo].[spx_Set_Inv_Reported]    Script Date: 16/04/2025 05:59:17 „ ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[spx_Set_Inv_Reported]
@InvID int=null

AS
BEGIN
 update  tblSalesOrderHdr set  Reported=1


	
WHERE  fInvID = @InvID		
END
GO


