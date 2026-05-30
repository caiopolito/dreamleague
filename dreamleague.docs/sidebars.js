// @ts-check

/** @type {import('@docusaurus/plugin-content-docs').SidebarsConfig} */
const sidebars = {
  docs: [
    'index',
    {
      type: 'category',
      label: 'Introduction',
      collapsed: false,
      items: ['getting-started', 'setup'],
    },
    {
      type: 'category',
      label: 'Architecture',
      items: ['architecture', 'database-structure'],
    },
    {
      type: 'category',
      label: 'Configuration',
      items: ['configuration'],
    },
    {
      type: 'category',
      label: 'Features',
      items: ['website-features', 'match-lifecycle', 'server-commands'],
    },
    {
      type: 'category',
      label: 'Support',
      items: ['donation'],
    },
  ],
};

module.exports = sidebars;
