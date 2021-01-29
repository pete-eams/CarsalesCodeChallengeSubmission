import React, { Component } from 'react'
import Select from 'react-select'
import '../custom.css'

const greekSpeakingOptions = [
  { value: 0, label: 'Yes' },
  { value: 1, label: 'No' }
]

const carPreferenceOption = [
  { value: 0, label: 'Sports Vehicle' },
  { value: 1, label: 'Family Vehicle' },
  { value: 2, label: 'Tradie Vehicle' },
  { value: 3, label: 'No preference' }
]

export default class UserForm extends Component {
  constructor(props) {
    super(props)
    this.state = { 
      loading: false, 
      allocatedSalesPerson: null, 
      langPref: greekSpeakingOptions[0], 
      carPref: carPreferenceOption[0] 
    }
    this.allocate = this.allocate.bind(this)
  }

  componentDidMount() {
    this.setState({ allocatedSalesPerson: 'Default' })
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
        <h1 id="allocateLabel">Allocate Me A Salesperson</h1>
        <div className="language-preference-form-group">
          <p>Do you speak Greek?</p>
          <Select 
            options={greekSpeakingOptions} 
            value={this.state.langPref}
            onChange={value => this.handleLangPrefChange(value)}
          />
        </div>
        <div className="car-preference-form-group">
          <p>What kind of car are you looking for?</p>
          <Select 
            options={carPreferenceOption} 
            value={this.state.carPref}
            onChange={value => this.handleCarPrefChange(value)}
          />
        </div>
        <p aria-live="polite">
          Currently selected Language Preference:{' '}
          <strong>{this.state.langPref.label}</strong>
        </p>
        <p aria-live="polite">
          Currently selected Car Prefernece:{' '}
          <strong>{this.state.carPref.label}</strong>
        </p>
        <p aria-live="polite">
          Currently allocated salesperson:{' '}
          <strong>{this.state.allocatedSalesPerson}</strong>
        </p>
        <button className="btn btn-primary" onClick={this.allocate}>
          Allocate!
        </button>
      </div>
    )
  }

  async getAllocation() {
    this.setState({ loading: true, allocatedSalesPerson: null })
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(
        { 
          CarPreference: this.state.carPref.value,
          LanguagePreference: this.state.langPref.value 
        }),
    }
    const response = await fetch('allocate', requestOptions)
    const data = await response.json()

    this.setState({ allocatedSalesPerson: data.name })
  }
}