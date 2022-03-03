# Trilogic.EasyARGS
A very simple library for parsing command line arguments.

Arguments may take the form:
  /q or -q
  /f=filename
  /f="file path"
  -p=
  /server=name
  -server="some other name"
  /port=22

This library provides a standardized command line arg functionality for applications that accept an array of parameters.
