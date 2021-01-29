import React, { Component } from 'react'
import UserForm from './UserForm.js'
import Status from './Status.js'

export class Allocate extends Component {
  static displayName = Allocate.name

  constructor(props) {
    super(props)
    this.state = {}
  }

  render() {
    return (
      <div>
        <UserForm />
        <Status />
      </div>
    )
  }
}
