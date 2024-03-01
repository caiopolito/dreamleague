import * as Yup from 'yup'

import useUtils from './useUtils'
import { PHONE_MASKS } from '../constants/defaults'

const useYup = () => {
  const { isBlank, getOnlyNumber } = useUtils()

  const phone = Yup.string().test({
    name: 'phone',
    message: 'Insira um telefone inválido.',
    test: (value) => {
      let valid = true

      if (value) {
        valid = value.length === PHONE_MASKS.phone.length
      }
      return valid
    },
  })

  const cellphone = Yup.string().test({
    name: 'cellphone',
    message: 'Insira um telefone inválido.',
    test: (value) => {
      let valid = true

      if (value) {
        valid = value.length === PHONE_MASKS.cellphone.length
      }
      return valid
    },
  })

  const phoneOrCellphone = Yup.string().test({
    name: 'phoneOrCellphone',
    message: 'Insira um telefone/celular inválido.',
    test: (value) => {
      let valid = true

      if (!isBlank(value)) {
        const number = getOnlyNumber(value)
        const phoneMask = getOnlyNumber(PHONE_MASKS.phone)
        const cellphoneMask = getOnlyNumber(PHONE_MASKS.cellphone)

        valid = number.length === phoneMask.length || number.length === cellphoneMask.length
      }
      return valid
    },
  })

  return {
    phone, cellphone, phoneOrCellphone,
  }
}

export default useYup
