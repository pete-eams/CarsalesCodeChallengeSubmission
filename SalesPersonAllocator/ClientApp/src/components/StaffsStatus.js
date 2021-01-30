import React, { Component } from 'react'
import { Grommet } from 'grommet'
import { grommet } from 'grommet'
import { DataTable } from 'grommet'

const columns = [
  {
    property: 'name',
    header: 'Name',
    primary: true,
  },
  {
    property: 'available',
    header: 'Available',
  },
]
const groupColumns = [...columns]

export default class StaffsStatus extends Component {
  constructor(props) {
    super(props)
    this.state = {
      loading: true,
      items: [],
    }
  }

  componentDidMount() {
    this.getItems()
    this.timer = setInterval(() => this.getItems(), 300)
  }

  componentWillUnmount() {
    clearInterval(this.timer)
    this.timer = null
  }

  getItems() {
    fetch('status')
      .then((result) => result.json())
      .then((result) => this.updateItems(result))
  }

  updateItems(result) {
    var newData = []
    result.forEach((item) => {
      newData.push({
        name: item.name,
        available: item.isAllocated ? 'Busy' : 'Available',
      })
    })
    this.setState({ items: newData, loading: false })
  }

  render() {
    return (
      <div>
        <h4>Our Specialists Statuses</h4>
        {this.state.loading ? (
          <p>Awaiting Staffs Status...</p>
        ) : (
          <Grommet theme={grommet}>
            <DataTable columns={groupColumns} data={this.state.items} />
          </Grommet>
        )}
      </div>
    )
  }
}
