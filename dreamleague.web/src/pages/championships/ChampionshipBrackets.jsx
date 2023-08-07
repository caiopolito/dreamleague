import React from 'react'
import { Bracket } from 'react-brackets'
import PropTypes from 'prop-types'

const ChampionshipBrackets = ({ bracket }) => {
  console.log(bracket)
  return (<Bracket rounds={bracket.rounds} />)
}

ChampionshipBrackets.propTypes = {
  bracket: PropTypes.object.isRequired,
}

export default ChampionshipBrackets
