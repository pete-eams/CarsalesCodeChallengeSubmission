# CarsalesCodeChallengeSubmission
[![Build Status](https://travis-ci.com/pete-eams/CarsalesCodeChallengeSubmission.svg?branch=master)](https://travis-ci.com/pete-eams/CarsalesCodeChallengeSubmission)

### About/Requirements

This project is a submission to the Carsales coding challenge.
The requirements for the project can be found here : https://github.com/mjhornecarsales/carsales-challenge

### Demo

The demo of this submission is hosted via Azure cloud services and can be found below
https://salespersonallocator20210131001743.azurewebsites.net/

### Configuration

The software is configured to de-allocate the sales people 5 minutes after they have been allocated by default, to simulate their work being done. This can be configured off by changing the value of **DeAllocationTimeSec** in **BehaviourConfiguration.json** to **null**.

### Design Consideration

The backend is designed with *SOLID* principle and extensibility in mind, using *chain of responsibility design pattern* to handle customer preferences and choosing appropriate salespeople, it should be able to handle changes in requirements quite well (ie. adding/removing/modifying assignment use cases). 
