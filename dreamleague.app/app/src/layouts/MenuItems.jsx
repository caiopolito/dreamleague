import React from 'react'
import PropTypes from 'prop-types'
import { NavLink } from 'react-router-dom'
import SVG from 'react-inlinesvg'

import {
  List,
  Divider,
  ListItem,
  Typography,
  ListItemIcon,
} from '@mui/material'

import { MENUS } from 'constants/defaults'
import { colors } from 'theme'

const MenuItems = ({ classes, isMobile, onSelectItem }) => (
  <List>
    {MENUS.map((menu, index) => (
      <React.Fragment key={`menu-${isMobile ? 'mobile' : ''}-${index}`}>
        <ListItem
          button={!isMobile}
          to={menu.route}
          component={NavLink}
          activeClassName="active"
          className={classes.navItem}
          onClick={() => onSelectItem(false)}
        >
          <ListItemIcon className={classes.navIcon}>
            <SVG
              width={24}
              height={34}
              src={menu.icon}
              fill={colors.secondary.main}
              stroke={colors.secondary.main}
            />

            <Typography color="secondary">
              {menu.label}
            </Typography>
          </ListItemIcon>
        </ListItem>

        <Divider orientation="vertical" variant="middle" flexItem className={classes.divider} />
      </React.Fragment>
    ))}
  </List>
)

MenuItems.propTypes = {
  classes: PropTypes.object.isRequired,
  isMobile: PropTypes.bool.isRequired,
  onSelectItem: PropTypes.func,
}

MenuItems.defaultProps = {
  onSelectItem: () => { },
}

export default MenuItems
