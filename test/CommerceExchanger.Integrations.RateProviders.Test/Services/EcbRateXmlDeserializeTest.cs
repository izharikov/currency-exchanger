﻿using System.IO;
using System.Text;
using CommerceExchanger.Integrations.RateProviders.Services;
using Xunit;

namespace CommerceExchanger.Integrations.RateProviders.Test.Services
{
    public class EcbRateXmlDeserializeTest
    {
        private const string XmlResponse =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<message:GenericData xmlns:message=\"http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message\" xmlns:common=\"http://www.sdmx.org/resources/sdmxml/schemas/v2_1/common\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:generic=\"http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic\" xsi:schemaLocation=\"http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message https://sdw-wsrest.ecb.europa.eu:443/vocabulary/sdmx/2_1/SDMXMessage.xsd http://www.sdmx.org/resources/sdmxml/schemas/v2_1/common https://sdw-wsrest.ecb.europa.eu:443/vocabulary/sdmx/2_1/SDMXCommon.xsd http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic https://sdw-wsrest.ecb.europa.eu:443/vocabulary/sdmx/2_1/SDMXDataGeneric.xsd\">\r\n    <message:Header>\r\n        <message:ID>fdbab9ab-0a9e-4715-b1c5-f7199f70a7d3</message:ID>\r\n        <message:Test>false</message:Test>\r\n        <message:Prepared>2020-09-16T10:58:04.384+02:00</message:Prepared>\r\n        <message:Sender id=\"ECB\"/>\r\n        <message:Structure structureID=\"ECB_EXR1\" dimensionAtObservation=\"TIME_PERIOD\">\r\n            <common:Structure>\r\n                <URN>urn:sdmx:org.sdmx.infomodel.datastructure.DataStructure=ECB:ECB_EXR1(1.0)</URN>\r\n            </common:Structure>\r\n        </message:Structure>\r\n    </message:Header>\r\n    <message:DataSet action=\"Replace\" validFromDate=\"2020-09-16T10:58:04.384+02:00\" structureRef=\"ECB_EXR1\">\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"ARS\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"88.874\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"89.2554\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"AUD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.6327\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.6219\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"BGN\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.9558\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.9558\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"BRL\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"6.3109\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"6.2272\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"CAD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.5641\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.5634\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"CHF\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.0768\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.0768\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"CNY\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"8.0987\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"8.0526\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"CZK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"26.66\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"26.827\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"DKK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"7.4398\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"7.4396\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"DZD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"152.6007\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"152.729\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"GBP\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"0.9219\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"0.92095\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"HKD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"9.2041\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"9.2164\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"HRK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"7.5368\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"7.5375\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"HUF\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"357.65\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"357.68\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"IDR\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"17671.49\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"17653.67\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"ILS\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"4.0807\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"4.0675\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"INR\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"87.3415\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"87.5205\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"ISK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"160\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"160.6\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"JPY\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"125.82\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"125.39\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"KRW\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1404.73\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1400.73\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"MAD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"10.8875\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"10.944\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"MXN\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"25.1792\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"24.9307\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"MYR\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"4.9232\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"4.912\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"NOK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"10.6933\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"10.6963\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"NZD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.7739\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.7675\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"PHP\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"57.587\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"57.509\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"PLN\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"4.4504\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"4.4461\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"RON\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"4.858\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"4.8578\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"RUB\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"89.5924\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"89.1013\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"SEK\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"10.4178\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"10.404\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"SGD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.6207\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.6163\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"THB\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"37.172\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"37.079\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"TRY\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"8.8997\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"8.9023\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"TWD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"34.7516\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"34.7077\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"USD\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"1.1876\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"1.1892\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n        <generic:Series>\r\n            <generic:SeriesKey>\r\n                <generic:Value id=\"FREQ\" value=\"D\"/>\r\n                <generic:Value id=\"CURRENCY\" value=\"ZAR\"/>\r\n                <generic:Value id=\"CURRENCY_DENOM\" value=\"EUR\"/>\r\n                <generic:Value id=\"EXR_TYPE\" value=\"SP00\"/>\r\n                <generic:Value id=\"EXR_SUFFIX\" value=\"A\"/>\r\n            </generic:SeriesKey>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-14\"/>\r\n                <generic:ObsValue value=\"19.7876\"/>\r\n            </generic:Obs>\r\n            <generic:Obs>\r\n                <generic:ObsDimension value=\"2020-09-15\"/>\r\n                <generic:ObsValue value=\"19.5669\"/>\r\n            </generic:Obs>\r\n        </generic:Series>\r\n    </message:DataSet>\r\n</message:GenericData>";

        [Fact]
        public void EcbRateProvider_Parse_ShouldBeValid()
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(XmlResponse));
            var result = EcbRateProvider.Deserialize(memoryStream);
            Assert.NotEmpty(result.DataSet.Series);
        }
    }
}