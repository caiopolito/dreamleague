import * as Yup from 'yup'

const YUP_MESSAGE = {
  email: 'E-mail inválido.',
  invalid: 'Valor inválido.',
  required: 'Campo obrigatório.',
  minNumber: ({ min }) => `Valor deve ser maior que ${min}`,
  min: ({ min }) => `Digite no mínimo ${min} caracteres`,
  max: ({ max }) => `Digite no máximo ${max} caracteres`,
}

Yup.setLocale({
  mixed: {
    default: YUP_MESSAGE.invalid,
    notType: YUP_MESSAGE.invalid,
    required: YUP_MESSAGE.required,
  },
  number: {
    min: YUP_MESSAGE.minNumber,
    integer: YUP_MESSAGE.invalid,
  },
  string: {
    max: YUP_MESSAGE.max,
    min: YUP_MESSAGE.min,
    email: YUP_MESSAGE.email,
  },
})

export default YUP_MESSAGE
