CREATE PROCEDURE GetById(@id as int)
AS
BEGIN
    SELECT * from Product  WHERE Product.id = @id
END;