import React, { Component } from 'react'
import Select from 'react-select'
import '../custom.css'

const greekSpeakingOptions = [
  { value: 0, label: 'Yes' },
  { value: 1, label: 'No' },
]

const carPreferenceOption = [
  { value: 0, label: 'Sports Vehicle' },
  { value: 1, label: 'Family Vehicle' },
  { value: 2, label: 'Tradie Vehicle' },
  { value: 3, label: 'No preference' },
]

export default class UserForm extends Component {
  constructor(props) {
    super(props)
    this.state = {
      loading: false,
      langPref: greekSpeakingOptions[0],
      carPref: carPreferenceOption[0],
      openPopUp: false,
      allocationMessage: null,
    }
    this.allocate = this.allocate.bind(this)
  }

  allocate() {
    this.getAllocation()
  }

  handleLangPrefChange(value) {
    console.log(value)
    this.setState({ langPref: value })
  }

  handleCarPrefChange(value) {
    console.log(value)
    this.setState({ carPref: value })
  }

  render() {
    return (
      <div>
        <h1>Find Me A Sale Specialist</h1>
        <div>
          <p>Do you speak Greek?</p>
          <div className="combo-box">
            <Select
              options={greekSpeakingOptions}
              value={this.state.langPref}
              onChange={(value) => this.handleLangPrefChange(value)}
            />
          </div>
        </div>
        <div>
          <p>What kind of car are you looking for?</p>
          <div className="combo-box">
            <Select
              options={carPreferenceOption}
              value={this.state.carPref}
              onChange={(value) => this.handleCarPrefChange(value)}
            />
          </div>
        </div>
        <div className="row">
          <button className="btn btn-primary" onClick={this.allocate}>
            Allocate!
          </button>
          <p className="allocate-result">{this.state.allocationMessage}</p>
        </div>
      </div>
    )
  }

  updateAllocationMessage(name) {
    if (name !== '')
      this.setState({ allocationMessage: `You have been allocated ${name}` })
    else
      this.setState({
        allocationMessage: 'All salespeople are busy. Please wait.',
      })
  }

  clearAllocationMessage() {
    this.setState({
      allocationMessage: '',
    })
  }

  resetTimer() {
    clearTimeout(this.timer)
    this.timer = setTimeout(() => this.clearAllocationMessage(), 5000)
  }

  updateMessageAndResetTimer(salesPersonName) {
    this.updateAllocationMessage(salesPersonName)
    this.resetTimer()
  }

  populateRequest() {
    return {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        CarPreference: this.state.carPref.value,
        LanguagePreference: this.state.langPref.value,
      }),
    }
  }

  async getAllocation() {
    this.setState({ loading: true })
    const requestOptions = this.populateRequest()

    fetch('allocate', requestOptions)
      .then((response) => response.json())
      .then((data) => {
        this.updateMessageAndResetTimer(data.name)
      })
  }
}
