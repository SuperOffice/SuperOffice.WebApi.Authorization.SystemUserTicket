# Sample SuperOffice WebApi System User Authorization

This sample authorization type is based on the [SuperOffice.WebApi.Authorization.SystemUserTicket]() authorization type.
It demonstrates how to implement both IAuthorization and IAddHeaders.

## IAuthorization

Responsible for populating the HTTP request Authorization header.

## IAddHeaders

Responsible for populating additional custom HTTP request headers.
