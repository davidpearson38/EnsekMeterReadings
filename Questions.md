### Questions

1. What is a duplicate entity?

This was initially coded with a composite key of AccountId and MeterReadingDateTime, therefore allowing multiple readings per Account but for different dates/times.

I ran out of time to code this logic properly because it would have meant having to add validation to check previously loaded readings for duplicates (and search for duplicates within the current upload).

I then questionned what a duplicate entity was, because it could mean several things, so opted to leave it as-is and discuss that later.

2. The API always returns 200 (OK). Should it?

What should the API return in the event that all rows are invalid?

I've hit this issue before where the payload contains multiple entries, yet the "resource" being uploaded is a MeterReading. So because you have multiple entries in your payload, what do you do when you have a mix of success/failure?

Currently it returns a payload containing the number of successes and a list of failure messages. This is quite flexible since it allows the consumer to parse the response and determine the number of failures, together with feedback about why they failed.

However it doesn't feel particularly RESTful since we are unable to provide a concrete success/fail for a single resource.

3. What timezone is the MeterReadingDateTime in?

This has just been coded assuming a generic/unknown timezone, which isn't ideal.

The format in the CSV suggests that this is "local" time, because it's not formatted in ISO format, but that's just an assumption.

Ideally i would specify the format in the CSV as ISO (i.e. 2021-08-23T21:31:58Z) and dictate that it must be a UTC time, since we can co-ordinate that across timezones.

Failing that, if the format is fixed, i'd prefer to save the date/time to the DB as a UTC value as this is more future-proof if we later decide to roll this API out in multiple timezones. Maybe the API could be extended to include a timezone column?

4. Validation of the MeterReading could be improved

It's currently using `int.Parse` to verify the MeterReading value. It's therefore not _quite_ meeting the requirement to enforce that the format must be NNNNN. Parsing a decimal will possibly truncate or round the value, where we should be rejecting it.