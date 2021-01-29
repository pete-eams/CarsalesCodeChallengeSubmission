import React from 'react';
import { Grommet } from 'grommet';
import { grommet } from 'grommet';
import { Box } from 'grommet';
import { DataTable } from 'grommet';
import { Meter } from 'grommet';
import { Text } from 'grommet';
import { nodeModuleNameResolver } from 'typescript';

const amountFormatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
  minimumFractionDigits: 2
});

const columns = [
  {
    property: 'name',
    header: "Name",
    primary: true
  },
  {
    property: 'available',
    header: 'Available'
  }
];

const DATA = [
  {
    name: 'Alan',
    location: '',
    date: '',
    available: 'Yes',
    percent: 0,
    paid: 0
  },
  {
    name: 'Bryan',
    location: 'Fort Collins',
    date: '2018-06-10',
    available: true,
    percent: 30,
    paid: 1234
  },
  {
    name: 'Chris',
    location: 'Palo Alto',
    date: '2018-06-09',
    available: true,
    percent: 40,
    paid: 2345
  },
  {
    name: 'Eric',
    location: 'Palo Alto',
    date: '2018-06-11',
    available: true,
    percent: 80,
    paid: 3456
  },
  {
    name: 'Doug',
    location: 'Fort Collins',
    date: '2018-06-10',
    available: true,
    percent: 60,
    paid: 1234
  },
  {
    name: 'Jet',
    location: 'Palo Alto',
    date: '2018-06-09',
    available: true,
    percent: 40,
    paid: 3456
  },
  {
    name: 'Michael',
    location: 'Boise',
    date: '2018-06-11',
    available: true,
    percent: 50,
    paid: 1234
  },
  {
    name: 'Tracy',
    location: 'San Francisco',
    date: '2018-06-10',
    available: true,
    percent: 10,
    paid: 2345
  }
];

const groupColumns = [...columns];
const first = groupColumns[0];
groupColumns[0] = { ...groupColumns[1] };
groupColumns[1] = { ...first };
groupColumns[0].footer = groupColumns[1].footer;
delete groupColumns[1].footer;

const GroupedDataTable = () => (
  <Grommet theme={grommet}>
    <DataTable columns={groupColumns} data={DATA} groupBy='name' sortable />
  </Grommet>
);

export default GroupedDataTable