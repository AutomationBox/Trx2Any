This project aims at converting VSTS test automation reports (trx reports) to various formats

### Trx2Any

Ever wish to publish your visual studio test automation report (.trx) into a more readable report to your managers/analysts as a part of test reporting (or daily CI builds). Excel format, maybe.

**Trx2Any** is a conversion tool which aims to convert complex test reports to multiple formats. **Trx2Any** aims to be an extensible tool which can convert a report from multiple formats to multiple formats.

**Trx2Any** is based on MEF framework and supports extensibility of both Exportable formats and source formats. At this point in time, Trx2Any only supports trx format as source format and Excel 2007 format as exportable format.

**Trx2Any** has a command line interface which makes its integration with various CI servers, an easy affair.

One of the features of **Trx2Any** is to analyze Test Result XML (trx) files, which contains multiple hierarchies of ordered test cases into a flat structure and makes it available to Test Analysts for analysis in form of a readable test report.

This framework also gives capability to the test automation experts to write their own transformers and implement their custom formats (exportable format) to produce custom reports.


***


### What is Trx2Any?
Trx2Any wishes to be a tool to convert test results reports (e.g. trx reports ) to various formats. More can be found at [Home](https://github.com/TestAutomator/Trx2Any/blob/master/README.md).
 
### How do I use Trx2Any and how to Integrate it with CI?
Trx2Any is meant to provide easy operation for converting test reports to analysis friendly reports and aims at providing seamless integration with CI.
Detailed documentation about usage and CI Integration is here.
 
### How to Extend the Framework for Parsing a New Test Results File Format?
So you found that you have a report format that you couldn’t parse using Trx2Any. But still you want to save yourself from writing a conversion logic, that converts your data and write to a usable format (of course, format supported by Trx2Any). Here, Trx2Any saves your day. Trx2Any supports extensions to the Parse-able formats. A step by step guide is here.
 
### How to Extend the Framework for exporting Test Results to a new file format?
Didn’t like the formats Trx2Any outputs to? Creating your own formats and using it with Trx2Any is simple. Follow these steps.
 
### Where does Trx2Any go from here?
We will soon update and publish our Product backlog. But surely, future looks promising.
