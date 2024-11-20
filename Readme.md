# Orders Service

## Requirements

The service should offer the following functionalities:
- Get list of all orders.
- Get an order by id. List all order information, including the total amount of order.
- Create an order. The following information will be provided to the service:
  - Customer FirstName (required info)
  - Customer LastName (required info)
  - Customer Type. There are three customer types, Standard, Premium and Vip.
  - Customer Email
  - Address street (required info)
  - City (required info)
  - PostalCode (required info)
  - Country (required info)
  - List of order items including Name and Price (required info)
- Update an order. Once order is created, only the address can be updated.
- Complete order. Mark a given order as completed.
- Delete order.
- Add an item to existing order.
- Delete an item from existing order.

### Additional Rules:
- Orders should include information for the date of creation and completion.
- Order items can be only deleted, no update is allowed.
- Orders should contain a unique number. The business requirement is this identitifier to be sequential and to always contain 6 digits (with leading zeroes).


### Addendum 1
- Once the order is completed, no longer can be updated, neither items can be added or removed.

### Addendum 2
- We need the ability to assign discount to orders. We need GrandTotal info on order.
- The discount values should be as follows:
	- CustomerType Standard 10%
	- CustomerType Premium 15%
	- CustomerType Vip 20%
	
### Addendum 3
- We have new CustomerType = President, and for this type the discount will be 25%.
