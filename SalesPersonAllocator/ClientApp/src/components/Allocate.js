import React, { Component } from 'react'
import UserForm from './UserForm.js'
import StaffsStatus from './StaffsStatus.js'

export class Allocate extends Component {
  static displayName = Allocate.name

  constructor(props) {
    super(props)
    this.state = {}
  }

  render() {
    return (
      <div className="allocate-container">
        <div className="form">
          <UserForm />
        </div>
        <div className="status">
          <StaffsStatus />
        </div>
      </div>
    )
  }
}
