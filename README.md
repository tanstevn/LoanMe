## LoanMe

This repository has its own documentation on what are the requirements and how to run both applications.

See the `README.md` files in the respective folders (`Backend` and `Frontend`) for instructions.

## Pre-requisites

Before you begin testing both applications -- doing something from client-side to server-side, please make sure that you have installed:

- Postman

This is due because you need to execute `POST api/loan/draft` endpoint first.

## Getting started

Once both applications are running, open in your local browser the localhost port 3001 to access the client-side.

```bash
http://localhost:3001
```

This will give you an introduction to the home page of the client-side application. You will see there the tech stacks used to get this repository build and running.

## Database tables

- Users
- DraftLoans
- Products
- BlacklistMobiles
- BlacklistEmailDomains
- ActiveLoans

## Execution flow

This flow is by order.

1. Run `POST api/loan/draft` endpoint
	- You can get the request body structure on `http://localhost:5145/scalar/v1` and reference on its model to know what type is used per request field.
	
	Note: This endpoint is assumed to be called by a third-party. The response you will get after a successful command run is HTML format of the page where it will be redirected.
		To visualize the scenario, you can just go to your local browser and navigate to `http://localhost:3001/loans?id={draftLoanId}` so it will redirect you to what where will be the customer be redirected as well.
		Replace `{draftLoanId}` with the `Id` value from the `DraftLoans` table. You can access through your SQL Server.

2. Calculate quote in client-side
	- You can adjust the inputs displayed from the calculator quote page or simply calculate the quote directly.
	- The calculate quote button will call `PUT api/loan/draft/update` endpoint to trigger update for the drafted loan created initially and redirect you to your quote `/loans/quote` page.

3. Your quote in client-side
	- This will be displayed once you have clicked the calculate quote button from the calculator quote page.
	- Upon render, it will call `GET api/loan/calculator/quote` endpoint to fetch the necessary details to be displayed in the page.
	- The `edit` button will just make you go back to calculator page.
	- The `apply now` button will call `POST api/loan` endpoint to store in `ActiveLoans` table the loan request details. This will then redirect you to success page `loans/success` and will display the application number generated and stored from the table for future reference of the user.