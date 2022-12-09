# Description

You are given a solution (LINQ.zip) containing the task project and the tests. Your task is to implement methods in LinqTask class following instructions from the list below until the tests are green. 

Queries: 

1. Select the customers whose total turnover (the sum of all orders) exceeds a certain value. 

2. For each customer make a list of suppliers located in the same country and the same city. Compose queries with and without grouping. 

3. Find all customers with the sum of all orders that exceed a certain value. 

4. Select the clients, including the date of their first order. 

5. Repeat the previous query but order the result by year, month, turnover (descending) and customer name. 

6. Select the clients which either have:
    - a. non-digit postal code
    - b. undefined region
    - c. operator code in the phone is not specified (does not contain parentheses) 

7. Group the products by category, then by availability in stock with ordering by cost. 

8. Group the products by “cheap”, “average” and “expensive” following the rules:
    - a. From 0 to cheap inclusive
    - b. From cheap exclusive to average inclusive
    - c. From average exclusive to expensive inclusive 

9. Calculate the average profitability of each city (average amount of orders per customer) and average rate (average number of orders per customer from each city). 

10. Build a string of unique supplier country names, sorted first by length and then by country.

## Scoreboard:

1-3 stars – 5 green tests. 

4 stars - 8 green tests. 

5 stars - all tests are green. 
