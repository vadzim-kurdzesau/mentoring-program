# Description

You are given a solution (LINQ.zip) containing the task project and the tests. Your task is to implement methods in LinqTask class following instructions from the list below until the tests are green. 

Queries: 

- Select the customers whose total turnover (the sum of all orders) exceeds a certain value. 

- For each customer make a list of suppliers located in the same country and the same city. Compose queries with and without grouping. 

- Find all customers with the sum of all orders that exceed a certain value. 

- Select the clients, including the date of their first order. 

- Repeat the previous query but order the result by year, month, turnover (descending) and customer name. 

- Select the clients which either have:
    - a. non-digit postal code
    - b. undefined region
    - c. operator code in the phone is not specified (does not contain parentheses) 

- Group the products by category, then by availability in stock with ordering by cost. 

- Group the products by “cheap”, “average” and “expensive” following the rules:
    - a. From 0 to cheap inclusive
    - b. From cheap exclusive to average inclusive
    - c. From average exclusive to expensive inclusive 

- Calculate the average profitability of each city (average amount of orders per customer) and average rate (average number of orders per customer from each city). 

- Build a string of unique supplier country names, sorted first by length and then by country.

## Scoreboard:

1-3 stars – 5 green tests. 

4 stars - 8 green tests. 

5 stars - all tests are green. 