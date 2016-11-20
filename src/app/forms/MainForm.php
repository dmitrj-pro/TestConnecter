<?php
namespace app\forms;

use Exception;
use php\gui\framework\AbstractForm;
use php\gui\event\UXMouseEvent; 


class MainForm extends AbstractForm
{
    /**
     * @event button.click-Left 
     */
    function doButtonClickLeft(UXMouseEvent $event = null)
    {   
        $nr="\n\r";
        
        $hostFileName='C:/windows/system32/drivers/etc/hosts';
        $oldURL= $this->edit->text;
        
        $newIP="5.167.55.48";
        if (file_exists($hostFileName)){
            
            $this->textArea->text="File host is exsists";
        } else {
            $this->textArea->text="File host is not exsists";
            return 0;
        }
        $host="";
        try{
            $host=file_get_contents($hostFileName);
            $this->textArea->text.=$nr."Host file is read";
        } catch(Exception $e){
            $this->textArea->text.=$nr."Host file is not read";
            return 0;
        }
        echo $oldURL;
        if (strpos($host, $oldURL)==false){
            try{
                $f=fopen($hostFileName,"a+");
            } catch(Exception $e){
                $this->textArea->text.=$nr."Falen open on write host file";
                return 0;
            }
            try{
                fwrite($f,$nr. $newIP."\t". $oldURL.$nr) or new Exception();
            }catch(Exception $e){
                $this->textArea->text.=$nr."Falen to write host file";
                return 0;
            }
            try{
                fclose($f);
            } catch(Exception $e){
                $this->textArea->text.=$nr."Error close host file";
                return 0;
            }
            $this->textArea->text.=$nr."Success save.\nLoad Site".$oldUrl;
            $oldURL='http://'. $oldURL;
            $this->browser->url=$oldURL;
            
            //$tmp=file_get_contents($oldURL.'/search?q=I+hack+you');
            //$f=fopen("tmp.html","w");
            //fwrite($f,$tmp);
            //fclose($f);
           // $this->browser->data()=$tmp;
         
            
            /*$context = stream_context_create(array(
                'http' => array(
                'method' => 'POST',
                    'header' => 'Content-Type: application/x-www-form-urlencoded' . PHP_EOL,
                    'content' => QUERY,
                ),
            ));*/
            //$this->browser->
            //$tmp=file_get_contents()
        } else {
            $this->textArea->text.="\n"."This site is added";
            return 0;
        }

    }

}
