create database ExchangeRate;

use ExchangeRate;
create table ExchangeRates(
    id int DENTITY(1,1),
    rate smallmoney,
    from nvarchar(3),
    to nvarchar(3),
    date date,
    provider nvarchar(5)
)

