Trx2Any
=======

This project aims at converting VSTS test automation reports (trx reports) to various formats

Trx2Any

Ever wish to publish your visual studio test automation report (.trx) into a more readable report to your managers/analysts as a part of test reporting (or daily CI builds). Excel format, maybe.

Trx2Any is a conversion tool which aims to convert complex test reports to multiple formats. Trx2Any aims to be an extensible tool which can convert a report from multiple formats to multiple formats.

Trx2Any is based on MEF framework and supports extensibility of both Exportable formats and source formats. At this point in time, Trx2Any only supports trx format as source format and Excel 2007 format as exportable format.

Trx2Any has a command line interface which makes its integration with various CI servers, an easy affair.

One of the features of Trx2Any is to analyze Test Result XML (trx) files, which contains multiple hierarchies of ordered test cases into a flat structure and makes it available to Test Analysts for analysis in form of a readable test report.

This framework also gives capability to the test automation experts to write their own transformers and implement their custom formats (exportable format) to produce custom reports.
