DELETE [dbo].[ClothingPictures]
WHERE Id IN (
    SELECT TOP 20
        T2.Id
    FROM [dbo].[Clothing]  AS T1 
    JOIN [dbo].[ClothingPictures] AS T2 
    ON T1.Id = T2.ClothingId
    WHERE IsPickup = 1 AND PickupDt < DATEADD(MONTH, -6, GETDATE())
)

SELECT 
        T1.Id, T2.Id
    FROM [dbo].[Clothing]  AS T1 
    JOIN [dbo].[ClothingPictures] AS T2 
    ON T1.Id = T2.ClothingId
    WHERE IsPickup = 1 AND PickupDt < DATEADD(MONTH, -6, GETDATE())

-- SELECT COUNT(0) FROM [dbo].[ClothingPictures] WITH (NOLOCK)
