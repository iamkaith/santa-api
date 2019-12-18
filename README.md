# Santa List API 

This API was created to understand how the Identity framework operates and get familiar with JWT.

## Background Information

Santa is coming to town! Children who wants to get a present from Santa has to register online and leave Santa their location info so he can swing by with presents.
Children are only able to view and update their profile information. 
Santa has full "admin" privileges and are able to see a list of children who signed up, can edit, and is able to delete profiles if necessary.

## Santa (aka Admin)

- Read, update, delete Children
- See list of children

## Children (aka Regular, Non-Admin users)

- Create profile (via registration)
- Update profile

### Notes

- Written in C#

- Uses ASP.NET framework

- Uses JWT access tokens