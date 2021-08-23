### Tech test for Ensek

I tried to follow a Clean Architecure pattern that i've been using for a while. The idea is to separate Domain concerns from Business implementation.

The WebAPI is built entirely on Domain classes and abstractions. The DI in `startup` wires up the Domain abstractions to Business concrete implementations. This way the UI is decoupled from the Business implementation and is purely working with abstractions.

I'm also a fan of separating the Data/Entities into their own classes rather than mapping Domain classes directly to the DB. I prefer to decouple the two types of data, where the "db" classes are decorated by EF annotations, but the Domain classes are just pure classes and know nothing about EF.

The repository masks the usage of these Data/Entities classes and always takes/returns Domain classes.

By following this pattern you end up with the UI only knowing about the Domain, and the business logic only dealing with Domain classes.

