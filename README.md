# A sample application used for demonstrating where EntityFrameworkCore.Triggered ads value

## Scenario
(only for demo purposes): We're implementing a book store. We should keep track of the following entities: Books, Customers, Purchases and Emails. There are a few requirements that we need to cover:
- When a book gets added/updated with a price of 0. All customers will automatically purchase this book for that price.
- When a customer signs up, all books with a price of 0 will automatically be purchased by that customer.
- When a customer signs up, we'll sent a welcome email to that customer automatically.
- When the customer makes a purchase, we'll send a purchase confirmation email to that customer automatically.
- When a customer signs up, we'll automatically assign Customer.SignupDate to the current date.
- When an email gets queued we'll automatically set the QueuedDate to now
- When an email gets sent out we'll automatically set the SentDate to now

## Implementations
There are 2 variants of the same sample application: `BookStoreSampleApp.Simple` and `BookStoreSampleApp.Triggered`. The former covers a traditional implementation of the given scenario wheras the latter embraced `EntityFrameworkCore.Triggered` to drive its business logic